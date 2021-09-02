# Movie Rental System

### Introduction

> This project simulates a movie rental system, that has an API to provide the logic to rent movies. 

Base language and concepts that you'll find in this project:

- written in C#.
- cqrs approach.
- using aloha library (https://github.com/walter-lopes/Aloha) - It's my own open source project to help developers in their productivity.
- hexagonal architecture base.
- unit of work approach to guarantee consistency.
- domain driven design concept [ubiquitous language, encapsulation, entities and value objects]
- notification pattern approach
- 
### Accesses

> admin access
admin user -> admin@gmail.com
admin password -> admin

> customer access
customer user -> customer@gmail.com
customer password -> customer
----------

## Structure

This project is structured in multi layers:

- API
 
 Is a first layer that is responsible for receive all request, controling authorization and authentication

 - Domain

 Is the heart of our application, all entities, abstraction and domain bussiness are there. This layer needs to align with ubiquitous language.

 - Application

Our intermediate layer, it is responsible for help us to map commands and queries to our domain and communicate with our data repository. Some bussiness logic are there as well.

- Infra

The last layer, it is responsible for data access and configurations.

## How can we test?

Some postman collections are on the /requests directory.


## Use cases


## Why cqrs approach

Cqrs is a pattern that help us to separate commands (writes) to queries (retrieves), this pattern is useful to understand what is happen in the system and what are the user intentions, is more explicit and imperative.
In the future is easier to create a database access segregation, just changing the query side for example.

## Why Aloha

Aloha is a library that helps other developers in their productivity, if you need to use cqrs approach, notification pattern, message brokers, aloha brings a several options to make your life better.

## Why unit of work

In this project we have a great example, accessing the class RentMovieCommandHandler from application layer, we can see that it needs to save a rent and clean the cart, this operation needs to be transactional,
in other words cart and rental need to update together, so unit of work approach help us to guarantee this consistency.

## Notification pattern

Errors need to be tracked and showed to our clients as clean as possible. Throw exceptions when you can validate the flow, it's not a good idea, exceptions generate I/O bounds and it is expensive for our system, 
using notification pattern we can "magical" track the errors without throw any exception, and our controller base can handle this notification and return a clean error to the client.

## Improvements

- Logging to facilitate system tracking.
- Use message brokers to help us with async payments and resilience.
- Rental managament status, we can build some background jobs to set expiration date in the rentals. helping us to control the status.


  
