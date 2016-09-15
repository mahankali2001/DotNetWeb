'use strict';

/* Services */
/*
var services = angular.module('ngdemo.services', ['ngResource']);

services.factory('UserFactory', function ($resource) {
    return $resource('/ngdemo/rest/app/user', {}, {
        query: {
            method: 'GET',
            params: {},
            isArray: false
        }
    })
    
});
*/

ngdemo.service('ngdemo.services', ['$http', '$location', function ($http, $location) {

    /*this.headers = {
    headers: {'OAuth': 'oauth_consumer_key=8737055b-0cc8-4977-99d2-573252fe9906&oauth_nonce=188465&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1384200442&oauth_version=1.0&oauth_signature=60Zy575Xv2oQDMo%2FGBUjcyXTF6Y%3D',
    'retailerCode' : 'XYZ_DEV_00',
    'appuserid' : 2,
    'cultureCode' : 'en-Us'
    }};

    this.GetUrl = function (methodName) {
    return "/ngdemo/rest/" + methodName;
    //return "http://localhost/MicroServiceFeatureName/1.0/" + methodName;
    },
    this.GetSetupGridConfig = function (callback) {
    var url = $('#hdnAppPath').val() + "/Angular/Setup/SetupGridConfig.json";
    $http.get(url).success(function (data) {
    var result = (data) || [];
    if (typeof callback == 'function') callback(angular.fromJson(result));
    });
    },
    this.GetUser = function (uid,callback) {    
    $http.get(this.GetUrl("app/users")+uid,this.headers).success(function (data) {
    var result = (data) || [];
    if (typeof callback == 'function') callback(angular.fromJson(result));
    })
    };
    this.GetUsers = function (callback) {
    $http.get(this.GetUrl("app/users"),this.headers).success(function (data) {
    var result = (data) || [];
    if (typeof callback == 'function') callback(angular.fromJson(result));
    });
    };
    this.SaveUser = function (user,callback) {
    $http.post(this.GetUrl("app/users/input"),user,this.headers).success(function (data, status, headers, config) {
    var result = (data) || [];
    if (typeof callback == 'function') callback(angular.fromJson(result));
    }).
    error(function (data, status, headers, config) {
    alert("NOT SENT");
    });
    };
    this.DeleteUser = function (uid,callback) {
    $http.get(this.GetUrl("app/users/delete/")+uid,this.headers).success(function (data) {
    var result = (data) || [];
    if (typeof callback == 'function') callback(angular.fromJson(result));
    });
    };
    this.CopyUser = function (uid,callback) {
    $http.get(this.GetUrl("app/users/copy/")+uid,this.headers).success(function (data) {
    var result = (data) || [];
    if (typeof callback == 'function') callback(angular.fromJson(result));
    });
    };*/

    this.GetUrl = function(methodName) {
        if ($location.absUrl().indexOf("web/user/") === -1) {
            return "web/user/" + methodName;
        }
        //return "web/user/" + methodName;
        return methodName;
        //return "http://localhost/MicroServiceFeatureName/1.0/" + methodName;
    },
    this.GetUsers = function(callback) {
        $http.get(this.GetUrl("GetUsers")).success(function(data) {
            var result = (data) || [];
            if (typeof callback == 'function') callback(angular.fromJson(result).GetUsersResult);
        });
    },
    this.GetUser = function(uid, callback) {
        var json = { uid: uid };
        var data = JSON.stringify(json);
        $http.post(this.GetUrl("GetUser"), data, this.headers).success(function(data) {
            var result = (data) || [];
            if (typeof callback == 'function') callback(angular.fromJson(result).GetUserResult);
        });
    };
    this.SaveUser = function (user, callback) {
        $http.post(this.GetUrl("SaveUser"), user, this.headers)
            .success(function (data, status, headers, config) {
                var result = (data) || [];
                if (typeof callback == 'function') callback(angular.fromJson(result).SaveUserResult);
            })
            .error(function (data, status, headers, config) {
                alert("NOT SENT");
            });
    };
} ]);

/*
services.factory("serviceFactory", function () {
    var mem = {},
    factory = {};

        factory.store = function (key, value) {
            mem[key] = value;
        };

        factory.get = function (key) {
            return mem[key];
        };

        return factory;
    });
*/