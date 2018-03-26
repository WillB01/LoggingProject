var id = document.getElementById("id");
var thread = document.getElementById("thread");
var searchBtn = document.getElementById("submit-search-btn");
var validationOutput = document.getElementById("validation-output");

id.addEventListener("keydown", function (evt) {
    
    var keycode = event.which;
    if (!(event.shiftKey == false && (keycode === 46 || keycode === 8 || keycode === 37 || keycode === 39 || (keycode >= 48 && keycode <= 57)))) {
        event.preventDefault();
        validationOutput.innerHTML = "Digits only"
    } else {
        validationOutput.innerHTML = ""
    }
   
});

thread.addEventListener("keydown", function (evt) {

    var keycode = event.which;
    if (!(event.shiftKey == false && (keycode === 46 || keycode === 8 || keycode === 37 || keycode === 39 || (keycode >= 48 && keycode <= 57)))) {
        event.preventDefault();
        validationOutput.innerHTML = "Digits only"
    } else {
        validationOutput.innerHTML = ""
    }

});



