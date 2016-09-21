function checkLength(e, minLength) {
    var el, elMsg;
    el = e.target;
    elMsg = document.getElementById('nameMsg');

    if (el.value.length < minLength) {
        elMsg.innerHTML = 'You must enter at least ' + minLength + ' characters or more!';
    } else {
        elMsg.innerHTML = '';
    }
}

var elUsername = document.getElementById('firstName');
elUsername.addEventListener('blur', function(e) {
  checkLength(e, 5);
}, false);
