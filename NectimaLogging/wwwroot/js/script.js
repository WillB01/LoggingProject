
//var el = document.querySelectorAll('.btn a');
//console.log(el);
//for (var i = 0; i < el.length; i++) {
//    el[i].addEventListener('click', function (e) {

//        alert("");
      
//    }, false);
    

//}
$(function () {
    $("#datepicker").datepicker({ dateFormat: 'yy-mm-dd' });
});

var advancedSearch = document.getElementById("advanced-btn");
var x = document.getElementById("advanced-search");
x.style.display = "none";
advancedSearch.addEventListener("click", function () {

   
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
});