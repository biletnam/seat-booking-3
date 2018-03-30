# Seat booking

This is a POC .net core application for booking seats. The intention behind this application is to explore and show off technologies like for instance SignalR, Akka.net, Jenkins, testing frameworks, Kubernetes, etc.

### Technologies in use
*

### Questions and/or feedback?
Feel free to create issues. I'll happily document further in the wiki or markdown files if something is unclear.

### TODO
- [ ] Web interface showing seat map (using [jQuery Seat Chart](https://github.com/mateuszmarkowski/jQuery-Seat-Charts)?)
- [ ] Allow user to select seats
- [ ] Lets users be able to see other users selections, similar to what Google Docs is showing when multiple people are inside a document at the same time. Use SignalR for this.
- [ ] By default SignalR only can communicate to the clients connected to that one server instance. Implement fan-out/scale-out for SignalR using Redis.