# ActiveMQ Module
## Setup

1. Download `apache-activemq-6.0.1-bin.zip` from https://activemq.apache.org/components/classic/download/ and unpack it.
2. Make sure JRE SDK is installed
3. Navigate to `\apache-activemq-6.0.1\bin\win64` and run `activemq.bat`. After that the following console appears:

<img width="862" alt="image" src="https://github.com/AlexandrYanochkin/ActiveMQ/assets/57268382/9f2360eb-9e68-4651-94ba-85e4306fa9bf">

4. Navigate to WebConsole /admin and use the following credentials: 
   - login: admin
   - password: admin

5. Add the following Queues:
   - `RequestorQueue`
   - `ReplierQueue` 

   and the following Topic: `PublishSubscribe`

## Tests

#RequestReply

https://github.com/AlexandrYanochkin/ActiveMQ/assets/57268382/b481dc1d-911f-4a65-ba51-74700cb87f8d

#PublishSubscribe

https://github.com/AlexandrYanochkin/ActiveMQ/assets/57268382/161e578c-7f00-407c-9143-08e18b58fcd2

#VirtualTopic

https://github.com/AlexandrYanochkin/ActiveMQ/assets/57268382/f02545a9-9ce0-4535-82b8-d5d41cfc63c7

