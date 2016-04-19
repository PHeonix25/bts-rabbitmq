# Behind the Scenes - 2016-04-21
A part of the "Microservices Architecture" series of talks:

**Dealing with (temporarily) offline dependencies**

This is a repo containing my demo for the upcoming event about "stuff going offline"

## Goals
We want to demo Polly in use when (for example) 
the connection to RabbitMq drops.

## Usage
Through the course of the presentation I will step the 
audience through:

1. Setting up a simple console app to emulate a multi-threaded environment
2. Catching (and logging) exceptions along the way
3. Dealing with Task Cancellation (and its exceptions)
4. Connecting to RabbitMq (simplest implementation)
5. Taking RabbitMq offline and watch exceptions bubble up
6. Implement Polly Retry Policies and make sure we can continue.

Also desired, but not currently implemented;

* Walk people through the various types of Policies you can make, explaining them as we go

## AppVeyor
[![Build status](https://ci.appveyor.com/api/projects/status/h5osvji62oc66c7a?svg=true)](https://ci.appveyor.com/project/phermens-coolblue/bts-rabbitmq)