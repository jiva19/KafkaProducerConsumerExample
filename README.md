# 🚀 Kafka Integration & Infrastructure-as-Code Suite

## **Overview**
This repository contains a comprehensive **C# .NET** implementation of an Apache Kafka producer and consumer ecosystem. The primary objective of this project is to solve the **"Integration Challenge"** inherent in distributed systems: ensuring that code interacting with a message broker actually works against a real instance of that broker, rather than a simulated or "mocked" environment.

---

## **🏗️ Architectural Components**

### **The Producer Engine**
The `Producer2` class is built on the **Confluent.Kafka** client, providing a static method to dispatch messages asynchronously.
* **Reliable Delivery:** It returns a `DeliveryResult`, allowing the calling application to verify the **partition** and **offset** assigned by the broker—critical for logging and auditing in production systems.

### **The Consumer Service**
The `Consumer2` class implements a robust polling mechanism to ingest data from Kafka topics.
* **Consumer Groups:** Utilizes `test-consumer-group` to demonstrate Kafka’s horizontal scaling capabilities.
* **Resilient Offsets:** By setting `AutoOffsetReset.Earliest`, the consumer ensures no data is lost, allowing it to "replay" or catch up on messages if the service restarts.
* **Error Handling:** Includes a dedicated `ConsumeException` block to handle network jitter or broker unavailability gracefully.

---

## **🧪 High-Fidelity Testing (The "Crown Jewel")**
Testing Kafka usually requires a pre-installed broker, which makes tests "flaky" and difficult to run in CI/CD environments. This project solves that using **Testcontainers**.

* **Dynamic Orchestration:** Using `Testcontainers.Kafka`, the suite automatically pulls the Confluent Docker image and starts a real broker on demand.
* **OneTimeSetUp Lifecycle:** The Kafka container is started once per fixture and disposed of immediately after, optimizing performance and preventing "ghost" containers.
* **Round-Trip Assertion:** The main test case validates the entire pipeline: it produces a message, waits for the Broker to commit, and then consumes it back to verify 100% data integrity.

---

## **🛠️ Technical Stack**

| Category | Technology |
| :--- | :--- |
| **Language** | C# / .NET |
| **Messaging** | Confluent.Kafka |
| **Testing** | NUnit |
| **Infrastructure** | Testcontainers for .NET (Docker) |
| **Environment** | Confluent Platform (CP-Kafka) |

---

## **🏆 Key Achievements**
* **Real Infrastructure Testing:** Successfully moved away from "Mocking" to validate real-world broker behavior.
* **Async Mastery:** Demonstrated advanced C# patterns including `Task.Run` and `ProduceAsync`.
* **Clean Disposal:** Implemented industry-standard disposal patterns for containerized resources to ensure environment stability.
