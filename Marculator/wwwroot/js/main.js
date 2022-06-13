$(document).ready(function () {
    $('.add').on('click', function (event) {
        var html = '<div class="oneLine mb-3"> <input type="text" class="form-control forName" required> <input type="text" class="form-control forCount" required> </div>';
        $("#parent-div").prepend(html);
    });
}); 

function removeItem(){
    let menu = document.getElementById('parent-div');
    menu.removeChild(menu.lastElementChild);
}   