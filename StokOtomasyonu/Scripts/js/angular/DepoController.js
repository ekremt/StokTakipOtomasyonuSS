
myStok.controller('DepoController', ($scope, $http) => {

    $http({
        url: "/Depo/List",
        method: 'GET'

    }).then(function successCallback(response) {
        console.log(response.data)
        $scope.depo = response.data;
    }, function errorCallback(response) {
        // called asynchronously if an error occurs
        // or server returns response with an error status.
    });

    $scope.addFormSubmit = (depo) => {
        $http({
            method: 'POST',
            url: '/Depo/Create',
            data: JSON.stringify(depo),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            if (response.data == 'kayitYapilmadi') {

                $scope.notificationMessage = "Depo Eklenemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.depo.push(response.data);
                $('#DepoAddModal').modal('hide');
                $scope.notificationMessage = "Depo Eklendi";
                $scope.notification = true;
                $scope.itemInAddModal = angular.copy({});
            }
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

    }

    // Depo silme
    $scope.deleteClick = (item) => {
        $scope.itemInDeleteModal = item;

    }
    $scope.deleteFormSubmit = (depo) => {

        $http({
            method: 'POST',
            url: '/Depo/Delete',
            data: JSON.stringify(depo),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            if (response.data == 'silinemedi') {

                $scope.notificationMessage = "Depo Silinemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.depo.pop(response.data);
                $('#DepoDeleteModal').modal('hide');
                $scope.notificationMessage = "Depo Silindi";
                $scope.notification = true;
            }

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    // Depo düzenleme

    $scope.editClick = (item) => {
        $scope.itemInModal = item;

    }

    $scope.editFormSubmit = (depo) => {

        $http({
            method: 'POST',
            url: '/Depo/Edit',
            data: JSON.stringify(depo),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            $('#DepoEditModal').modal('hide');
            $scope.notificationMessage = "Depo Güncellendi"
            $scope.notification = true;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }
    //Depodaki ürünlerin gelmesi
    $scope.urunlerClick = (item) => {
        $scope.itemInViewUrun = item;
        $http({
            url: "/Depo/DepoUrun",
            method: 'POST',
            data: JSON.stringify($scope.itemInViewUrun),
            headers: { 'content-type': 'application/json' }

        }).then(function successCallback(response) {
            console.log(response.data)
            $scope.urunInDepo = response.data;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });


    }
});