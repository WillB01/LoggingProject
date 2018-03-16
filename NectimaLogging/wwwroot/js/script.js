
var el = document.querySelectorAll('.active a');
console.log(el);
for (var i = 0; i < el.length; i++) {
    el[i].addEventListener('click', function (e) {
        
        el.style.color = "red";
      
    }, false);
    

}


