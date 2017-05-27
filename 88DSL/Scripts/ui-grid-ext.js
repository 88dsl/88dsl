
$scope.$watch('pagingOptions', function (newVal, oldVal) {

    var isChanged = false;
    if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
        isChanged = true
    }

    if (newVal !== oldVal && newVal.pageSize !== oldVal.pageSize) {
        isChanged = true
    }

    if (isChanged) {
        $scope.loadPagingData($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);
    }
}, true);


$scope.loadPagingData = function (pageSize, currentPage) {


    var req = {
        method: 'POST',
        url: '/DirectLine/GetPesrons;',
        headers: {
            'Content-Type': undefined
        },
        data: { test: 'test' }
    }

    $http(req).then(function (res) {
        aler("sdf");
        $scope.listData = res.data;
    });

}
    

