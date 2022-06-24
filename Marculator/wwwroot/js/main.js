function addInputValues() {
    var k = 0;
    var input = document.getElementsByClassName("forCount");
    var price = document.getElementsByClassName("forPrice");
    for (var i = 0; i < input.length; i++) {
        var a = price[i].value * input[i].value;
        k = k + parseInt(a);
    }

    document.getElementById("total").innerHTML = `Jami summa: <br/> <span style="font-size: 22px;">${k}</span> so'm`;
};

function sortProducts() {
    var input, filter, form, oneLine, a, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    form = document.getElementById("myForm");
    oneLine = form.getElementsByClassName("oneLine");
    for (i = 0; i < oneLine.length; i++) {
        a = oneLine[i].getElementByClassName("forCount")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            oneLine[i].style.display = "";
        } else {
            oneLine[i].style.display = "none";
        }
    }
}

