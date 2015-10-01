(function(app) {
    'use strict';

    app.directvie('sideBar', sideBar);

    function sideBar() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/scripts/spa/layout/sideBar.html'
        };
    };


})(angular.module('common.ui'));