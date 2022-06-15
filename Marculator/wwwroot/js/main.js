$(document).ready(function () {
    $('.add').on('click', function (event) {
        var html = '<div class="oneLine mb-3"><select class="form-select forName" asp-for="Things[tr].Name" asp-items="@(new SelectList(Model.Things, nameof(Thing.Name), nameof(Thing.Name)))"> </select > <input type="number" class="form-control forCount" required asp-for="Things[tr].Count"> @(tr++;)</div>';
        $("#parent-div").append(html);
    });
}); 

function removeItem(){
    let menu = document.getElementById('parent-div');
    menu.removeChild(menu.lastElementChild);
}   