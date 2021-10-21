# .Net Core applications with Apache Kafka integration as a consumer and a receiver

## Development Guide

### Docker 
Install Docker (https://docs.docker.com/engine/install/)

#### Zookeeper
Keeps track of a brocker(a topic) in Kafka.

Run this command to bring zookeeper image `docker pull confluentinc/cp-zookeeper:latest`

ZooKeeper configuration (https://docs.confluent.io/platform/current/installation/docker/config-reference.html#zk-configuration)

#### Kafka
Run this command to bring Kafka image on docker `docker pull confluentinc/cp-kafka`

Kafka configuration(https://docs.confluent.io/platform/current/installation/docker/config-reference.html#confluent-ak-configuration)


## Environmentthanks Setup 

1. We should create a network in a docker since we want to put both containers in the same network
`docker network create kafka-demo`
2. Run Zookepper
`docker run -d --network=kafka-demo --name=zookeeper -e ZOOKEEPER_CLIENT_PORT=2181 -e ZOOKEEPER_TICK_TIME=2000 -e ZOOKEEPER_SYNC_LIMIT=2 -p 2181:2181 confluentinc/cp-zookeeper`
3. Run kafka
`docker run -d -p 9092:9092 --network=kafka-demo --name=kafka -e KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 -e KAFKA_BROKER_ID=2 -e KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 confluentinc/cp-kafka`

## Run applications