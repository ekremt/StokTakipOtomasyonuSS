myStok.controller('LoginController', ($scope,$http) => {
    $scope.login = function (user) {
        $scope.master = angular.copy(user);
       /* $scope.LoginData = {
            kullanici_adi: '',
            Password: ''
        }  */
        $http({
            method: 'POST',
            url: '/Login/Index',
            data: JSON.stringify(user), 
            headers: { 'content-type': 'application/json' }  
        }).then(function successCallback(response) {
            console.log("ok")
            $window.location.reload();
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };
   
});
