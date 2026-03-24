Kafka Integration & Infrastructure-as-Code Testing Suite
Overview
This repository contains a comprehensive C# implementation of an Apache Kafka producer and consumer ecosystem. The primary objective of this project was to solve the "Integration Challenge" associated with distributed systems: ensuring that code interacting with a message broker actually works against a real instance of that broker, rather than just a simulated environment.

Architectural Components
1. The Producer Engine
The Producer2 class is built on the Confluent.Kafka client. It provides a static method to dispatch messages asynchronously.

Key Feature: It returns a DeliveryResult, allowing the calling application to verify the partition and offset assigned by the broker, which is critical for logging and auditing in production systems.

2. The Consumer Service
The Consumer2 class implements a robust polling mechanism.

Consumer Groups: It utilizes test-consumer-group to demonstrate how Kafka manages horizontal scaling.

Offset Management: By setting AutoOffsetReset.Earliest, the consumer is designed to be resilient, capable of "replaying" or catching up on messages if it goes offline.

Error Handling: Includes a ConsumeException block to handle network jitter or broker unavailability gracefully.

3. High-Fidelity Testing with Testcontainers
The "crown jewel" of this project is the integration test suite. Testing Kafka usually requires a pre-installed broker, which makes tests flaky and hard to run in a CI/CD environment like GitHub Actions.

Dynamic Orchestration: Using Testcontainers.Kafka, the code automatically pulls the Confluent Kafka Docker image, starts a Zookeeper-less (or bundled) broker, and provides a dynamic BootstrapAddress.

OneTimeSetUp Lifecycle: To ensure performance, the Kafka container is started once per test fixture and disposed of immediately after, preventing "hanging" Docker processes and saving resources.

The Round-Trip Assertion: The main test case (Should_Produce_And_Consume_Message) validates the entire pipeline. It sends a message via the Producer, waits for the Broker to commit it, and then pulls it back via the Consumer to ensure the string value remains identical.

Technical Stack
Language: C# / .NET

Messaging: Confluent.Kafka

Testing: NUnit

Infrastructure: Testcontainers for .NET (Docker)

Environment: Confluent Platform (CP-Kafka)

Key Achievements
Successfully moved away from "Mocking" to "Real Infrastructure" testing.

Demonstrated mastery of asynchronous C# patterns (Task.Run, ProduceAsync).

Implemented clean disposal patterns for containerized resources.
