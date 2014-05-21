var ngApp = angular.module('ngApp', [
	'ngRoute',
	'ngAppControllers'
]);

ngApp.config(['$routeProvider',
	function($routeProvider) {
		$routeProvider.
			when('/stream', {
				templateUrl: 'Scripts/partials/_stream.html',
				controller: 'StreamController'
			}).
			when('/home', {
				templateUrl: 'Scripts/partials/_home.html',
				controller: 'HomeController'
			}).
			otherwise({
				redirectTo: '/stream'
			});
	}]);