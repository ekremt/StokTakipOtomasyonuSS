
myStok.controller('StokController', ($scope, $http) => {

    function stokList() {

    
    $http({
        url: "/Stok/List",
        method: 'GET'

    }).then(function successCallback(response) {
        console.log(response.data)
        $scope.stok = response.data;
    }, function errorCallback(response) {
        // called asynchronously if an error occurs
        // or server returns response with an error status.
    });
    }
    stokList();
    function isyeriList() {

        $http({
            method: 'GET',
            url: '/Isyeri/List'
        }).then(function successCallback(response) {
            $scope.isyeriList = angular.copy(response.data);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
      
    }
    function depoList() {
        $http({
            method: 'GET',
            url: '/Depo/List'
        }).then(function successCallback(response) {
            $scope.depoList = angular.copy(response.data);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
       
    }
    function urunList() {
        $http({
            method: 'GET',
            url: '/Urun/List'
        }).then(function successCallback(response) {
            $scope.urunList = angular.copy(response.data);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }
    //İş yeri şeçilirken ürünler ona göre gelir
    $scope.isyeriChange = (isyeri_id) => {
        $http({
            method: 'GET',
            url: '/Urun/List'
        }).then(function successCallback(response) {

            var list = _.filter(angular.copy(response.data), ['isyeri_id', isyeri_id]);
                $scope.urunListChange = list ;

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }





    //Stok ekleme modalı açılırken
        $scope.addClick = (item) => {
       
           
            depoList() 
            isyeriList()
    }


    $scope.addFormSubmit = (stok) => {
        $http({
            method: 'POST',
            url: '/Stok/Create',
            data: JSON.stringify(stok),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            if (response.data == 'kayitYapilmadi') {

                $scope.notificationMessage = "Stok Eklenemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                stokList();
                $('#StokAddModal').modal('hide');
                $scope.notificationMessage = "Stok Eklendi";
                $scope.notification = true;
                $scope.itemInAddModal = angular.copy({});
            }
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

    }

    // Stok silme
    $scope.deleteClick = (item) => {
        $scope.itemInDeleteModal = item;

    }
    $scope.deleteFormSubmit = (stok) => {
        console.log(stok)
        $http({
            method: 'POST',
            url: '/Stok/Delete',
            data: JSON.stringify(stok),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            if (response.data == 'silinemedi') {

                $scope.notificationMessage = "Stok Silinemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.stok.pop(response.data);
                $('#StokDeleteModal').modal('hide');
                $scope.notificationMessage = "Stok Silindi";
                $scope.notification = true;
            }

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    // Stok düzenleme

    $scope.editClick = (item) => {
        $scope.itemInEditModal = item;
        urunList()
        depoList()
        isyeriList()
    }

    $scope.editFormSubmit = (stok) => {

        $http({
            method: 'POST',
            url: '/Stok/Edit',
            data: JSON.stringify(stok),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            stokList();
            $('#StokEditModal').modal('hide');
            $scope.notificationMessage = "Stok Güncellendi"
            $scope.notification = true;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

});