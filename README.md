# Microservices labs

## Team
The project developed by team #5 : 
- Denys Liienko: Transaction service(
   [backend src](https://github.com/DeniesLie/microservices-labs/tree/main/src/backend/TransactionService), 
   [k8s config](https://github.com/DeniesLie/microservices-labs/tree/main/src/k8s/transaction-service),
   [dockerhub](https://hub.docker.com/repository/docker/denysl1enk0/transactionservice)), 
- Viktor Zozuliuk: Storage service(
   [backend src](https://github.com/DeniesLie/microservices-labs/tree/main/src/backend/StorageService), 
   [k8s config](https://github.com/DeniesLie/microservices-labs/tree/main/src/k8s/storage-service),
   [dockerhub](https://hub.docker.com/repository/docker/zozuliuk22/storageservice))
- Vynnyk Vitalii: Item service(
   [backend src](https://github.com/DeniesLie/microservices-labs/tree/main/src/backend/ItemService), 
   [k8s config](https://github.com/DeniesLie/microservices-labs/tree/main/src/k8s/item-service),
   [dockerhub](https://hub.docker.com/repository/docker/vynnykv/item-service))
- Kalchenko Yehor: Employee service(
   [backend src](https://github.com/DeniesLie/microservices-labs/tree/main/src/backend/EmployeeService), 
   [k8s config](https://github.com/DeniesLie/microservices-labs/tree/main/src/k8s/employee-service),
   [dockerhub](https://hub.docker.com/repository/docker/fundowakin/employeeservice))
- Shared: 
   - Angular client (
     [src](https://github.com/DeniesLie/microservices-labs/tree/main/src/frontend/client-app),
     [k8s config](https://github.com/DeniesLie/microservices-labs/tree/main/src/k8s/client),
     [docker hub](https://hub.docker.com/repository/docker/denysl1enk0/mslabsngclient)
   )

## Lab2
1) У рамках даної лабораторної роботи було додано CУБД SQL Server для кожного із сервісів в середовищі k8s 
   і реалізовано CRUD операції.

   Всі yaml файли, що пов'язані із конфігурацією БД зберігаються в директоріях */src/k8s/(service-name)/db*.

   Кластер піднято в GKE (Google Kubernetes Engine). GKE одразу має 3 види Storage Class: standard, standard-rwo, 
   premium-rwo. Всі створені Persistent Volume Claim (PVC) використовують standart Storage Class (значення за замовченням)

   Оскільки файли даних бази даних SQL Server (mdf), файли логів (ldf) і TempDB налаштовані в різних томах зберігання, і методи доступу SQL Server до них   
   відрізняються: довільне читання/запис vs послідовне, тому на кожний тип файлів було виділено окремий PVC. </br>
   <img src="https://user-images.githubusercontent.com/59698344/198966881-d3f925ee-023c-4151-b288-a528b5774c0a.png" width="500"/>

   Ми не звертаємось до бд ззовні кластеру - тільки у його межах, тому для сервісів бд обрано тип ClusterIp
   <img src="https://user-images.githubusercontent.com/59698344/198967571-5cb81085-a780-44dc-8f26-f93c99c6be1c.png" width="500"/>

2) Для роботи з бд було використано ORM EF Core, яка підтримує створення міграцій. 
   [Приклад директорії із згенерованими міграціями](https://github.com/DeniesLie/microservices-labs/tree/main/src/backend/TransactionService/Migrations)

3) Міграції відбуваються під час запуску REST сервісів (Без використання Kubernetes Job або initContainer). Якщо бд знаходиться вже у  стані поточної міграції, то      ORM Entity Framework це побачить і не буде зайвий раз звертатися до бд </br>
   ```
   public static void ApplyMigrations(this WebApplication app)
       {
           using var scope = app.Services.CreateScope();
           var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

           if (dbContext is not null && app.Environment.IsProduction())
           {
               try
               {
                   dbContext.Database.Migrate();
               }
               catch (Exception ex)
               {
                   app.Logger.LogError($"Could not apply migrations. {ex.Message}");
                   throw;
               }
           }
       }
   ```

4) Сервіс було розширено: додано CRUD операції для основної сутності кожного сервісу
   <img src="https://user-images.githubusercontent.com/59698344/198973550-bf38432d-54fa-4bb7-839b-55723affcde6.png" width="700"/>
   <img src="https://user-images.githubusercontent.com/59698344/198973710-7a7f9263-e675-4c77-95c5-5ac3e4374cbd.png" width="700"/>
   <img src="https://user-images.githubusercontent.com/59698344/198973896-e8538a12-9e2e-4a31-9ff6-ade27b92fdb2.png" width="700"/>
   <img src="https://user-images.githubusercontent.com/59698344/198973981-f2337b57-3842-4d6b-be60-6632b0a23884.png" width="700"/>

Як підключитися до веб-клієнта і перевірити роботу додатку описано в секції Lab1 ↓↓↓

## Lab1 - Guideline
При виконанні лабораторної роботи було налаштовано кластер kubernetes за допомогою хмарного провайдера Google Cloud Platform.

*Сервіси і інгреси у віддаленому кластері:* </br> 
![image](https://user-images.githubusercontent.com/59698344/196053511-4b449cdd-f1b4-4285-afda-d4f5ac0bf830.png)
![image](https://user-images.githubusercontent.com/59698344/196053518-18d79469-bf9a-45a4-8d04-5f6820f05ea8.png)

1. Для коректної роботи клієнта необхідно в конфігурацію локальньої DNS додати запис про айпі кластера: </br>
   *35.226.68.76 microserviceslabs*
2. Для перевірки клієнта на ангулярі, потрібно звернутись до адреси *http://microserviceslabs/*. Обов'язково треба використовувати доменне ім'я, а не ip адресу, бо HTTP    запити з клієнта на сервіси робляться на **microserviceslasbs/api** і браузер може блокувати кросс доменні запити на сервіси 
3. Для перевірки роботи окремого API сервісу, потрібно звернутись до однієї з адрес:
    - *http://microserviceslabs/api/itemservice/api/items*
    - *http://microserviceslabs/api/storageservice/api/storages*
    - *http://microserviceslabs/api/transactionservice/api/transactions*
    - *http://microserviceslabs/api/employeeservice/api/employees/GetByStorage/1*
4. Щоб розгорнути локально у minikube
   - Перейти у папку *scripts*.
   - Виконати bash-скрипт прописавши команду *bash apply.sh*. Виконається *kubectl apply* для кожного сервісу
   - Звернутися вийде тільки до бекенд сервісів. Клієнт викликає бекенд через **http://microserviceslasbs/api...**, тому він працює тільки для клауду і з конфігурацією локального DNS. 
   - Для того, щоб видалити сервіси потрібно виконати bash-скрипт прописавши команду *bash delete.sh*
