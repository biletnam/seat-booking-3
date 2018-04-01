(function () {
    var app = angular.module('seat-app', []);

    app.controller('SeatController', function ($scope) {
        $scope.assignedAnimal = '';
        $scope.animals = [];
        
        var transport = signalR.TransportType.WebSockets;
        $scope.connection = new signalR.HubConnection(`http://${document.location.host}/seat`, { transport: transport });
        
        $scope.connection.on('animalAssigned', (assignedAnimal) => {
            console.log(`You've been assigned ${assignedAnimal}`);
            $scope.assignedAnimal = assignedAnimal;
            $scope.$apply();
        });

        $scope.connection.on('newAnimalInTheFlock', newAnimal => {
            console.log(`${newAnimal} arrived`);
            $scope.animals.push(newAnimal);
            $scope.$apply();
        });

        $scope.connection.on('animalKilled', killedAnimal => {
            console.log(`Flock thinning, ${killedAnimal} is no more.`);
            $scope.animals.splice($scope.animals.indexOf(killedAnimal), 1);
            $scope.$apply();
        });

        $scope.connection.on('updatedAnimalList', animalList => {
            $scope.animals = animalList;
            $scope.$apply();
        });

        $scope.connection.start();
        
        // https://stackoverflow.com/questions/3426404/create-a-hexadecimal-colour-based-on-a-string-with-javascript
        $scope.stringToColour = function (str) {
            var hash = 0;
            for (var i = 0; i < str.length; i++) {
                hash = str.charCodeAt(i) + ((hash << 5) - hash);
            }
            var colour = '#';
            for (var i = 0; i < 3; i++) {
                var value = (hash >> (i * 8)) & 0xFF;
                colour += ('00' + value.toString(16)).substr(-2);
            }
            return colour;
        }
    });
}());