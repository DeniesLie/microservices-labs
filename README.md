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
