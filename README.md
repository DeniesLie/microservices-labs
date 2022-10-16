# Microservices labs

## Team
The project developed by team #5 : Denys Liienko, Viktor Zozuliuk, Vynnyk Vitalii, Kalchenko Yehor.

## Lab1 - Guideline
При виконанні лабораторної роботи було налаштовано кластер kubernetes за допомогою хмарного провайдера Google Cloud.
Рішення реалізовано лише при використанні хмари, оскільки клієнт на Angular звертається до доменного ім'я microserviceslabs, а не до локального ім'я localhost.
1. Отже, необхідно в конфігурацію локальньої DNS додати запис про айпі кластера. 
    *35.226.68.76 microserviceslabs*
2. Перейти у папку *scripts*.
3. Виконати bash-скрипт прописавши команду *bash apply.sh*
4. Для перевірки клієнта, потрібно звернутись до адреси *http://microserviceslabs/*
5. Для перевірки роботи окремого API, потрібно звернутись до однієї з адрес:
    1 - *http://microserviceslabs/api/itemservice/api/items*
    2 - *http://microserviceslabs/api/storageservice/api/storages*
    3 - *http://microserviceslabs/api/transactionservice/api/transactions*
    4 - *http://microserviceslabs/api/employeeservice/api/employees/GetByStorage/12*
6. Для того, щоб видалити сервіси потрібно виконати bash-скрипт прописавши команду *bash delete.sh*