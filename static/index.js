const startTime = performance.now();
window.onload = function () {
    setTimeout(function () {
        document.getElementById('loading').style.display = 'none';
        document.getElementById('content').textContent = (performance.now() - startTime);
    }, Math.random() * 5000);
}

slowHide = function (elem) {
    setTimeout(function () {
        elem.style.display = 'none';
    }, Math.random() * 5000);
}