window.onload = function () {
    var modal = document.getElementById("myModal");

    // Get the button that opens the modal
    var btn = document.getElementById("myBtn");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 
    btn.onclick = function () {
        modal.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    function onCloseLoginClick() {
        $(function () {
            $('body').css('background-color', 'blue !important');
        });
        $('#loginModal').modal('hide');
        $('.modal-backdrop').remove();
    }

    function onCloseRegisterClick() {
        $('#registerModal').modal('hide');
        $('.modal-backdrop').remove();
    }
};


