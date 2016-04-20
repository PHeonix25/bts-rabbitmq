# Behind the Scenes - 2016-04-21
A part of the "Microservices Architecture" series of talks:

**Dealing with (temporarily) offline dependencies**

This is a repo containing my demo for the upcoming event about "stuff going offline"

## Goals
We want to demo Polly in use when, for example, the network connection drops.

## Usage
Through the course of the presentation I will step the 
audience through:

1. Setting up a simple console app to emulate a multi-threaded environment
2. Catching (and logging) exceptions along the way
3. Making a web request (simplest implementation)
4. Taking our network offline and watch exceptions bubble up
5. Implement Polly Retry Policies and make sure we can continue.

Also desired, but not currently implemented;

* Walk people through the various types of Policies you can make, explaining them as we go
* Show the RabbitMq branch and discuss how this can be used in similar, but different situations

## AppVeyor
[![Build status](https://ci.appveyor.com/api/projects/status/h5osvji62oc66c7a?svg=true)](https://ci.appveyor.com/project/phermens-coolblue/bts-rabbitmq)