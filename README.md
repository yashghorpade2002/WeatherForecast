<h1>Weather Forecast</h1> 

This repo mainly describes the use case of localstack for the demonstration of the aws services on your local machine. Here we have used amazon dynamoDb and SQS.

LocalStack- It's a tool which runs locally on our system and which can be used to run AWS services on our own system. This tool levreges the benefites of using the Services free of cost. Tool is mainly used to run CI/CD pipelines which makes it even function faster in development and testing environment.

In this project I have mainly focused on creating a container in docker and an image of localStack running into it. In the further process I have created a table into dynamoDb database and that table will hold the cityName, temperature, summary, Date into it. Additionally I have also created an SQS queue which is used for asynchronous communication between the application and the DynamoDb database.

A WebApi is exported into swaggerUI and which takes the temperature data and adds it to the DynamoDb database and also writes it to the SQS queue as well.

Find the demonstration video link below 👇</br></br>
https://drive.google.com/file/d/1YLHZ90nblxxPm-OVvl9Oq4OY4xCyKDyh/view?usp=sharing

The screenshots are attached below 👇</br></br>

<img width="614" alt="image" src="https://github.com/user-attachments/assets/fa00cee9-7f8f-44c2-866e-4decca460aee" />
<br>
<img width="614" alt="image" src="https://github.com/user-attachments/assets/2e8a1f02-2db3-417f-8d4c-31a42537310c" />
<br>
<img width="614" alt="image" src="https://github.com/user-attachments/assets/f4fdac9b-6f8c-4042-b691-bccc1ea82be0" />
