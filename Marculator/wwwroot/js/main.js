$('vaxaxa').change(function (){
        var k = 0;

        var input = document.getElementsByClassName('vaxaxa');

        for (var i = 0; i < input.length; i++) {
            var a = input[i];
            k = k + parseInt(a.value);
        }

        document.getElementById("total").innerHTML = k;
});

function removeItem(){
    let menu = document.getElementById('parent-div');
    menu.removeChild(menu.lastElementChild);
    x--;
}