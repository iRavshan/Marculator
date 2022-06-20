var x = 1;
$(document).ready(function () {
    $('.add').on('click', function (event) {
        var html = '<div class="oneLine mb-3"><select class="form-select forName" asp-for="Things[' + x + '].Name" asp-items="@(new SelectList(Model.Things, nameof(Thing.Name), nameof(Thing.Name)))"> </select > <input type="number" class="form-control forCount" required asp-for="Things[' + x +'].Count"></div>';
        $("#parent-div").append(html);
    });
    x++;
}); 

function removeItem(){
    let menu = document.getElementById('parent-div');
    menu.removeChild(menu.lastElementChild);
    x--;
}   