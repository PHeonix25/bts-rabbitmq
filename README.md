# Behind the Scenes - 2016-04-21
A part of the "Microservices Architecture" series of talks:

**Dealing with (temporarily) offline dependencies**

This is a repo containing my demo for the April 2016 event about "stuff going offline"

## Presentation Slides
You can find the presentation slides [here](http://www.slideshare.net/PHeonix25/behind-the-scenes-at-coolblue-april-2016-61234596)

## Polly
You can find out more information about Polly [here](https://github.com/App-vNext/Polly)

## Demo Goals
We want to demomstrate Polly in use when, for example, a network connection drops.

## Demo Usage
Through the course of the presentation I will step the audience through:

1. Setting up a simple console app to emulate a multi-threaded environment
2. Catching (and logging) exceptions along the way
3. Making a web request (simplest implementation)
4. Taking our network offline and watch exceptions bubble up
5. Implement Polly Retry Policies and make sure we can continue.

Also desired, but not currently implemented;

* Walk people through the various types of Policies you can make, explaining them as we go
* Show the RabbitMq branch and discuss how this can be used in similar, but different situations

## Branch Information
I know during the talk I spoke about how I moved the work into a `WebRequestsInstead` branch, but that has now been merged into `master`.

If you're interested in using Polly with RabbitMQ, then please check out the `RabbitMQ` branch instead.

## Disclaimer
**THIS IS NOT PRODUCTION-READY CODE**; 
Use it at your own risk, preferably just for reference.
 
If you burn your house down, or kill your cat, I am not at fault - I hope.

## CI Badges
[![Build status](https://ci.appveyor.com/api/projects/status/h5osvji62oc66c7a?svg=true)](https://ci.appveyor.com/project/phermens-coolblue/bts-rabbitmq)