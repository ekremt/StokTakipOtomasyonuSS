myStok.controller('LoginController', ($scope,$http) => {
    $scope.login = function (user) {
        $scope.master = angular.copy(user);
       /* $scope.LoginData = {
            kullanici_adi: '',
            Password: ''
        }  */
        $http({
            method: 'POST',
            url: '/',
            data: JSON.stringify(user), 
            headers: { 'content-type': 'application/json' }  
        }).then(function successCallback(response) {
            console.log("ok")
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };
   
});
