var ngAppControllers = angular.module('ngAppControllers', []);

ngAppControllers.controller('StreamController', ['$scope', '$http',
	function($scope, $http) {
		$http.get('/api/nH/').success(function (data) {
			$scope.list = data;
		});
	}]);

ngAppControllers.controller('HomeController', ['$scope', function() {}]);