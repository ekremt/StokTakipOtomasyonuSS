
myStok.controller('IsyeriController', ($scope, $http) => {

    $http({
        url: "/Isyeri/List",
        method: 'GET'

    }).then(function successCallback(response) {
        console.log(response.data)
        $scope.isyeri = response.data;
    }, function errorCallback(response) {
        // called asynchronously if an error occurs
        // or server returns response with an error status.
    });

    $scope.addFormSubmit = (isyeri) => {
        $http({
            method: 'POST',
            url: '/Isyeri/Create',
            data: JSON.stringify(isyeri),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            if (response.data == 'kayitYapilmadi') {

                $scope.notificationMessage = "İşyeri Eklenemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.isyeri.push(response.data);
                $('#IsyeriAddModal').modal('hide');
                $scope.notificationMessage = "İşyeri Eklendi";
                $scope.notification = true;
                $scope.itemInAddModal = angular.copy({});
            }
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

    }

    // İş yeri silme
    $scope.deleteClick = (item) => {
        $scope.itemInDeleteModal = item;

    }
    $scope.deleteFormSubmit = (isyeri) => {

        $http({
            method: 'POST',
            url: '/Isyeri/Delete',
            data: JSON.stringify(isyeri),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            if (response.data == 'silinemedi') {

                $scope.notificationMessage = "İşyeri Silinemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.isyeri.pop(response.data);
                $('#IsyeriDeleteModal').modal('hide');
                $scope.notificationMessage = "İşyeri Silindi";
                $scope.notification = true;
            }

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    // İş yeri düzenleme

    $scope.editClick = (item) => {
        $scope.itemInModal = item;

    }

    $scope.editFormSubmit = (isyeri) => {

        $http({
            method: 'POST',
            url: '/Isyeri/Edit',
            data: JSON.stringify(isyeri),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            $('#IsyeriEditModal').modal('hide');
            $scope.notificationMessage = "İşyeri Güncellendi"
            $scope.notification = true;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }
    $scope.calisanlarClick = (item) => {
        $scope.itemInViewUser = item;
        $http({
            url: "/Isyeri/IsyeriUser",
            method: 'POST',
            data: JSON.stringify($scope.itemInViewUser),
            headers: { 'content-type': 'application/json' }

        }).then(function successCallback(response) {
            console.log(response.data)
            $scope.userInIsyeri = response.data;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
        

    }
    $scope.urunlerClick = (item) => {
        $scope.itemInViewUrun = item;
        $http({
            url: "/Isyeri/IsyeriUrun",
            method: 'POST',
            data: JSON.stringify($scope.itemInViewUrun),
            headers: { 'content-type': 'application/json' }

        }).then(function successCallback(response) {
            console.log(response.data)
            $scope.urunInIsyeri = response.data;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });


    }
    $scope.stoklarClick = (item) => {
        $scope.itemInViewStok = item;
        $http({
            url: "/Isyeri/IsyeriStok",
            method: 'POST',
            data: JSON.stringify($scope.itemInViewStok),
            headers: { 'content-type': 'application/json' }

        }).then(function successCallback(response) {
            console.log(response.data)
            $scope.stokInIsyeri = response.data;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });


    }
});