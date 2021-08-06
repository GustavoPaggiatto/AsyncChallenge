# AsyncChallenge
Architectural Overview:
  1. DDD: Applying business rules on a dedicated layer (Service project). Furthermore, it is an anemic DDD, with POCOs end Business Services (personally I like working this way because I find myself better off isolating all the rules of an object in one place).
  2. Hexagonal: The project was designed to not overload the business layer, that is, it only deals with the business (single responsibility principle)!!!. For this I created auxiliary layers to implement the adapters, repositories, visitors and adapters patterns (application call then according to your need).
  3. Soc: With especific Domain (interfaces, POCO entities, results etc.) project we can separate contracts from implementations.
  4. IoC: We have a specific layer to configure the DI.
  5. MicroServices: API layer.
 Technologies:
  1. .Net Core 3.1.
  2. Unity Container (IoC).
  3. Storage Type: In memory (I used this because persisting the data in a database would take the focus off the application). Data is stored in static lists in the repository layer with lock access (concurrency).
  4. RabbitMQ: Message broker.
  5. Log4Net: Write logs.
  6. Swagger: For tests.
Things to improve:
  1. Implement circuit break when sending email to user.
  2. Do unity tests and do mocks (repositories, adapters etc).
OBSs:
  1. signPp requests are process in the async principle (Task).
  2. We have a background worker ("StudentSignUpWorker") in the API layer to read queue (better than create windows service to do this at the moment).
