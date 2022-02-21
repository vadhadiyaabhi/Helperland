﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function hide_policy() {
    document.getElementById('privacy-policy').style.display = "none";
}


$(document).ready(function () {
    $(window).scroll(function () {
        if (this.scrollY > 20) {
            $(".navbar").addClass("sticky");
            $(".nav-item > .button").addClass("button-color");
        }
        else {
            $(".navbar").removeClass("sticky");
            $(".nav-item > .button").removeClass("button-color");
        }
    });
        

    

    $('.menu-toggler').click(function () {
        $(this).toggleClass("active");
        $('.custom-navbar-menu').toggleClass("active");
    });


    $(".admin-submenu").click(function () {
        let id = this.id;
        let childId = id + "-item";
        console.log(id);
        $('#' + id).toggleClass('active');
        $('#' + childId).toggleClass('active');
        console.log(childId);
    });

    $(".login").click(function () {
        $("#blur").addClass("blur");
        $("#Login-Modal").addClass("active");
        $("#Password-Modal").removeClass("active");
    });

    $("#password-reset").click(function () {
        $("#blur").addClass("blur");
        $("#Login-Modal").removeClass("active");
        $("#Password-Modal").addClass("active");
    });

    $(".close-modal").click(function () {
        $("#blur").removeClass("blur");
        $(".Modal").removeClass("active");
    });

    $(".redirect").click(function () {
        loginModal = true;
        window.location.replace("http://localhost:5000/Home/Login");
        //openLoginModal();
    });

    $("#hiddenButton").hover(function () {
        $("#hiddenButton").css("color", "#026579");
    });
    $("#hiddenButton").mouseout(function () {
        $("#hiddenButton").css("color", "#0c8fa5");
    });

    $(".schedule-plan > .button").click(function () {
        $(".schedule-plan").css("display", "none");
        $(".your-details").css("display", "block");
    });

});

jQueryAjaxPost = form => {
    //console.log("method call");
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.invalid) {
                    $("#error").html("Oops!! Invalid Credentials");
                }
                else if (res.notactive) {
                    $("#error").html("User has not been varified...\n Click the below link to get verification email");
                    $("#hiddenEmail").attr('value', res.email);
                    //$("#hiddenName").attr('value', res.name);
                    //$("#hiddenId").attr('value', res.id);
                    $("#hiddenButton").css("display", "block");
                    //console.log(res);
                }
                else if (res.notapproved) {
                    $("#error").html("Sorry...!! Admin has not approved your account yet, Once he will, You'll be able to LogIn and take services, THANK YOU")
                }
                else if (res.notexist) {
                    $("#error1").html("No account exist with this email")
                }
                else if (res.mailsent) {
                    $("#success1").html("A password reset link has been sent to your registered email. Please use it to reset your password.")
                    $("#error1").html("");
                }
                else if (res.required) {
                    $("#error1").html("Email field is must.");
                }
                else if (res.reset) {
                    $("#reset-password").css("display", "block");
                }
                else if (res.reseterror) {
                    $("#error").html("Oops!! It seems like your link is not valid or get expired");
                    $("#reset-password").css("display", "none");
                }
                else if (res.zipInvalid) {
                    $("#postal-code-error").html("Please enter valid Postal Code");
                }
                else if (res.zipAvailable) {
                    $("#tab2").addClass("color");
                    $("#tab2").siblings().removeClass("active");
                    $(".setup-service").css("display", "none");
                    $(".schedule-plan").css("display", "block");
                    $("#tab2").addClass("active");
                }
                else if (res.zipCode && !res.zipAvailable) {
                    $("#postal-code-error").html("We're sorry, Currently no Service Provider is available at this " + res.zipCode + " Postal Code");
                }
                //console.log(res);
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    catch (e) {
        console.log(e);
    }

    return false;
};


add_extra_service = (ele, id) => {
    //console.log(ele);
    //console.log(id);
    $(ele).toggleClass("selected");
    if ($(id).is(':checked')) {
        $(id).attr("checked", "");
        $(id).checked = false;
    }
    else {
        $(id).attr("checked", "checked");
        $(id).checked = true;
        $(id).setAttribute(':checked');
    }
    
};



// ---------------------vanilla JS for pegination that can be customized based on requirnments----------------------

let tbody = document.querySelector('tbody');
let tr = tbody.getElementsByTagName('tr');
let select = document.querySelector('select');
let ul = document.querySelector('.pegination-pages');

let arrayTr = [];
for (let i = 0; i < tr.length; i++) {
    arrayTr.push(tr[i]);
}

//this three line with rowCount(e) will only work if you have single Select element in page
// select.onchange = rowCount;
// function rowCount(e){
// let limit = parseInt(e.target.value); => Write Inside function
//-------------------------------------------------

function rowCount() {
    let neil = ul.querySelectorAll('.pagination-page-list');
    neil.forEach(n => n.remove());
    let element = document.getElementById('pegination-select');
    let limit = parseInt(element.value);
    // console.log(e.target.tagName);
    // console.log(e.target.value);
    displaypage(limit);
    // document.write(e);
}

function displaypage(limit) {
    tbody.innerHTML = '';
    if (limit <= arrayTr.length) {
        for (let i = 0; i < limit; i++) {
            tbody.appendChild(arrayTr[i]);
        }

        document.getElementById('from-entry').innerHTML = '1';
        document.getElementById('to-entry').innerHTML = limit;
    }
    else {
        let extra = limit - arrayTr.length;
        console.log('extra');
        for (let i = 0; i < limit - extra; i++) {
            tbody.appendChild(arrayTr[i]);
        }

        document.getElementById('from-entry').innerHTML = '1';
        document.getElementById('to-entry').innerHTML = arrayTr.length;
    }

    buttonGenerator(limit);
}

displaypage(4);

function buttonGenerator(limit) {
    const nofTr = arrayTr.length;
    // console.log(nofTr);
    // console.log(limit);
    if (nofTr <= limit) {
        // console.log('display none');
        ul.style.display = 'none';
    }
    else {
        ul.style.display = 'flex';
        const nofPage = Math.ceil(nofTr / limit);

        for (let i = 1; i <= nofPage; i++) {
            let li = document.createElement('li');
            li.className = 'pagination-page-list';
            let a = document.createElement('li');
            a.className = 'pagination-page-list';
            a.setAttribute('data-page', i);
            li.appendChild(a);
            a.innerText = i;
            ul.insertBefore(li, ul.querySelector('.next'));

            li.onclick = e => {
                // console.log(e);
                let x = a.getAttribute('data-page');
                tbody.innerHTML = '';
                x--;
                let start = limit * x;
                let end = start + limit;
                if (end <= arrayTr.length) {

                }
                else {
                    end = arrayTr.length;
                }

                let page = arrayTr.slice(start, end);

                for (let i = 0; i < page.length; i++) {
                    let item = page[i];
                    tbody.appendChild(item);
                }

                document.getElementById('from-entry').innerHTML = start + 1;
                document.getElementById('to-entry').innerHTML = end;
            }
        }
    }

    // z=0;
    function nextElement() {
        // let y = parseInt(document.getElementById('from-entry').innerHTML)-1;
        // console.log(y);
        let z = parseInt(document.getElementById('from-entry').innerHTML) - 1;
        // console.log(z);
        // --------------------------------------------------------------
        if (z + limit > arrayTr.length || z > arrayTr.length) {
            z = -limit;
        }
        if (this.id == 'next') {
            z == arrayTr.length - limit ? (z = 0) : (z += limit);
            // console.log(z);
        }
        if (this.id == 'prev') {
            z == 0 ? (z = arrayTr.length - limit) : (z -= limit);
            z = z < 0 ? 0 : z;
            // console.log(z);
        }

        tbody.innerHTML = '';
        let d = z + limit > arrayTr.length ? arrayTr.length : z + limit;
        for (let c = z; c < d; c++) {
            tbody.appendChild(arrayTr[c]);
            // console.log(c);
        }
        document.getElementById('from-entry').innerHTML = z + 1;
        document.getElementById('to-entry').innerHTML = d;
        // console.log(z);
    }

    document.getElementById('prev').onclick = nextElement;
    document.getElementById('next').onclick = nextElement;
}


// ------------------------------------------------------------------------------------------------------------
