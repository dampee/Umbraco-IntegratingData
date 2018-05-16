angular.module("umbraco").controller("ImportDashboardController", function ($scope,userService) {
    var vm = this;
    var user = userService.getCurrentUser().then(function (user) {
        console.log(user);
        vm.UserName = user.name;
        vm.AllowImports = user.userType === "admin";
    });
    console.log(vm);
});