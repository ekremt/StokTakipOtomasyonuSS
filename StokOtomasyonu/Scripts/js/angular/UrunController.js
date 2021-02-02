myStok.controller('UrunController', ($scope, $http) => {
    $scope.notification = false;
    
    $http({
        url: "/Urun/List",
        method: 'GET'

    }).then(function successCallback(response) {
        console.log(response.data)
        $scope.urun = response.data;
    }, function errorCallback(response) {
        // called asynchronously if an error occurs
        // or server returns response with an error status.
    });
   
    //Urun Ekleme
    $scope.addFormSubmit = (urun) => {
        $http({
            method: 'POST',
            url: '/Urun/Create',
            data: JSON.stringify(urun),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            if (response.data == 'kayitYapilmadi') {

                $scope.notificationMessage = "Ürün Eklenemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.urun.push(response.data);
                $('#UrunAddModal').modal('hide');
                $scope.notificationMessage = "Ürün Eklendi";
                $scope.notification = true;
                $scope.itemInAddModal = angular.copy({});
            }
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

    }
    $scope.addClick = () => {
        $http({
            method: 'GET',
            url: '/Isyeri/List'
        }).then(function successCallback(response) {
            $scope.isyeriListAdd = angular.copy(response.data);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }
    // Urun Güncelleme

    $scope.editClick = (item) => {
        $scope.itemInEditModal = item;
        $http({
            method: 'GET',
            url: '/Isyeri/List'
        }).then(function successCallback(response) {
            $scope.isyeriListEdit = angular.copy(response.data);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

       
       

    }

    $scope.editFormSubmit = (urun) => {

        $http({
            method: 'POST',
            url: '/Urun/Edit',
            data: JSON.stringify(urun),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            $('#UrunEditModal').modal('hide');
            $scope.notificationMessage = "Ürün Güncellendi"
            $scope.notification = true;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    // İş yeri silme
    $scope.deleteClick = (item) => {
        $scope.itemInDeleteModal = item;

    }
    $scope.deleteFormSubmit = (urun) => {

        $http({
            method: 'POST',
            url: '/Urun/Delete',
            data: JSON.stringify(urun),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            if (response.data == 'silinemedi') {

                $scope.notificationMessage = "Ürün Silinemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.urun.pop(response.data);
                $('#UrunDeleteModal').modal('hide');
                $scope.notificationMessage = "Ürün Silindi";
                $scope.notification = true;
            }

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

});