// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function hide_policy() {
    document.getElementById('privacy-policy').style.display = "none";
}

$(document).ready(function () {
    $(window).scroll(function () {
        if (this.scrollY > 20) {
            $(".navbar").addClass("sticky");
        }
        else {
            $(".navbar").removeClass("sticky");
        }
    });

    $('.menu-toggler').click(function () {
        $(this).toggleClass("active");
        $('.custom-navbar-menu').toggleClass("active");
    });

    //$(document).on("click", function (e) {
    //    if ($(e.target).is('.custom-navbar-menu.active') === true && ($(e.target).is('.custom-navbar-menu') === false) {
    //        $('.custom-navbar-menu').toggleClass("active");
    //    }
    //});


    $(".admin-submenu").click(function () {
        let id = this.id;
        let childId = id + "-item";
        console.log(id);
        $('#' + id).toggleClass('active');
        $('#' + childId).toggleClass('active');
        console.log(childId);
    });
});



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

