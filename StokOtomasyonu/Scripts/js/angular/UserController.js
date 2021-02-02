

myStok.controller('UserController', ($scope, $http) => {
    $scope.notification = false;
    
    $http({
        url: "/User/List",
        method: 'GET'
       
    }).then(function successCallback(response) {
        console.log(response.data)
        $scope.user = response.data;
    }, function errorCallback(response) {
        // called asynchronously if an error occurs
        // or server returns response with an error status.
    });


    $scope.editClick = (item) => {
        $scope.userId = item.id;
        $scope.itemInModal = item;
        $http({
            method: 'GET',
            url: '/Isyeri/List'
        }).then(function successCallback(response) {
            $scope.isyeriList = angular.copy(response.data);
            
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

        //Yetki Checkboxa Uyarlanıyor
        
        if (item.is_admin) {
            $scope.checkBox = true;
        }
        else {
            $scope.checkBox = false;
        }

        

    };
    
    //Edit Form Post Ediiyor
    $scope.editFormSubmit = function (kullanici) {
        console.log(kullanici+"posting data....");
        $http({
            method: 'POST',
            url: '/User/Edit',
            data: JSON.stringify(kullanici),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            console.log("ok");
            $('#UserEditModal').modal('hide');
            $scope.notificationMessage="Kullanıcı Güncellendi"
            $scope.notification = true;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };

    //Notification Close
    $scope.closeNotification = () => {
      //  $scope.ntfCloseAnime = { 'right': '-200px' };
        $scope.notification = false;
    }

      //Şifre Güncellemesi
    $scope.sifreClick = true;
    $scope.sifreInputOld = false;
    $scope.sifreInputNew = false;
    $scope.sifreInputNewRepeat = false;
    $scope.editSifreClick = () => {
        $scope.sifreClick = false;
        $scope.sifreInputOld = true;

    }
    $scope.sifreClickOldSpan = () => {
        $scope.sifreInputOld = false;
        $scope.sifreInputNew = true;
    }
    $scope.sifreClickNewSpan = () => {
        $scope.sifreInputNew = false;
        $scope.sifreInputNewRepeat = true;
    }
    $scope.sifreClickNewRepeatSpan = (sifre) => {
        $scope.sifre = angular.copy(sifre);
        sifre.id = $scope.userId;

        if (sifre.sifreNew == sifre.sifreNewRepeat) {


            $http({
                method: 'POST',
                url: '/User/EditPassword',
                data: JSON.stringify(sifre),
                headers: { 'content-type': 'application/json' }
            }).then(function successCallback(response) {
                
                

                if (response.data == "sifreYanlis") {
                    $scope.ntfError = true;
                    $scope.notificationMessage = "Eski şifreniz yanlış.Lütfen tekrar deneyiniz."
                }
                else {
                    
                    $scope.notificationMessage = "Şifre Güncellendi"
                   
                }

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
        }
        else {
            $scope.ntfError = true;
            $scope.notificationMessage = "Yeni şifreniz tekrarı ile uyuşmuyor."
        }
        $scope.sifreClick = true;
        $scope.sifreInputOld = false;
        $scope.sifreInputNew = false;
        $scope.sifreInputNewRepeat = false;
        $scope.sifre.sifreOld = "";
        $scope.sifre.sifreNew = "";
        $scope.sifre.sifreNewRepeat = "";
        $scope.notification = true;
    }
    // Kullanıcı Ekleme
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
    //Add Form Post Ediiyor
    $scope.addFormSubmit = function (kullanici) {
        if (kullanici.sifre == kullanici.sifreRepeat) {
            $http({
                method: 'POST',
                url: '/User/Create',
                data: JSON.stringify(kullanici),
                headers: { 'content-type': 'application/json' }
            }).then(function successCallback(response) {
                console.log("ok");
                if (response.data == 'kayitYapilmadi') {

                    $scope.notificationMessage = "Kullanıcı Eklenemedi";
                    $scope.ntfError = true;
                    $scope.notification = true;
                }
                else {
                    console.log(response.data);
                    $scope.user.push(response.data);
                    $('#UserAddModal').modal('hide');
                    $scope.notificationMessage = "Kullanıcı Eklendi";
                    $scope.notification = true;
                    $scope.itemInAddModal=angular.copy({});
                }
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
        }
        else {
            $scope.notificationMessage = "Kullanıcı şifresi tekrarı yanlış";
            $scope.ntfError = true;
            $scope.notification = true;
        }
    };



    //Kullanıcı Silme
    $scope.deleteClick = (item) => {
        $scope.itemInDeleteModal = item;
    }
    //Delete Form Post Ediiyor
    $scope.deleteFormSubmit = function (kullanici) {

        $http({
            method: 'POST',
            url: '/User/Delete',
            data: JSON.stringify(kullanici),
            headers: { 'content-type': 'application/json' }
        }).then(function successCallback(response) {
            if (response.data == 'silinemedi') {

                $scope.notificationMessage = "Kullanıcı Silinemedi";
                $scope.ntfError = true;
                $scope.notification = true;
            }
            else {
                console.log(response.data);
                $scope.user.pop(response.data);
                $('#UserDeleteModal').modal('hide');
                $scope.notificationMessage = "Kullanıcı Silindi";
                $scope.notification = true;
            }
           
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

    }
});
