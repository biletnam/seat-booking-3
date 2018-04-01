# Seat booking

This is a POC .net core application for booking seats. The intention behind this application is to explore and show off technologies like for instance SignalR, Akka.net, Jenkins, testing frameworks, Kubernetes, etc.

### Technologies/libraries in use
* Front-end (quick and dirty)
  * jQuery
  * [jQuery Seat Chart](https://github.com/mateuszmarkowski/jQuery-Seat-Charts)
  * Bootstrap
  * AngularJS
  * Anonymous animals images lifted from [wayou's anonymous-animals](https://github.com/wayou/anonymous-animals) but original creator is Google.
* SignalR for websocket communication

### Questions and/or feedback?
Feel free to create issues. I'll happily document further in the wiki or markdown files if something is unclear.

### TODO
- [x] Web interface showing seat map
- [x] Allow user to individually select seats
- [x] 'Anonymous animal' a la Google Docs. Display all connected users so everybody can see the opponents fighting for the seats.
- [ ] Real time fight for the seats. Everybody should be able to see each others selections.
- [ ] By default SignalR only can communicate to the clients connected to that one server instance. Implement fan-out/scale-out for SignalR using Redis.
- [ ] Multipe seat charts, generated/stored on back-end.
- [ ] Checkout/payment