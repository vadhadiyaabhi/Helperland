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


});



// ---------------------vanilla JS for pegination that can be customized based on requirnments----------------------

let tbody = document.querySelector('tbody');
let tr = tbody.getElementsByTagName('tr');
let select = document.querySelector('select');
let ul = document.querySelector('.pegination-pages');

let arrayTr = [];
for(let i =0; i<tr.length; i++){
    arrayTr.push(tr[i]);
}

select.onchange = rowCount;
function rowCount(e){
    let neil = ul.querySelectorAll('.pagination-page-list');
    neil.forEach(n=>n.remove());
    let limit = parseInt(e.target.value);
    // console.log(e.target.tagName);
    // console.log(e.target.value);
    displaypage(limit);
    // document.write(e);
}

function displaypage(limit){
    tbody.innerHTML = '';
    if(limit <= arrayTr.length){
        for(let i=0; i<limit; i++){
            tbody.appendChild(arrayTr[i]);
        }
    }
    else{
        let extra = limit - arrayTr.length;
        console.log('extra');
        for(let i=0; i<limit-extra; i++){
            tbody.appendChild(arrayTr[i]);
        }
    }
    
    buttonGenerator(limit);
}

displaypage(4);

function buttonGenerator(limit){
    const nofTr = arrayTr.length;
    // console.log(nofTr);
    // console.log(limit);
    if(nofTr <= limit){
        // console.log('display none');
        ul.style.display = 'none';
    }
    else{
        ul.style.display = 'flex';
        const nofPage = Math.ceil(nofTr/limit);

        for(let i=1; i<=nofPage; i++)
        {
            let li = document.createElement('li');
            li.className = 'pagination-page-list';
            let a = document.createElement('li');
            a.className = 'pagination-page-list';
            a.setAttribute('data-page', i);
            li.appendChild(a);
            a.innerText = i;
            ul.insertBefore(li, ul.querySelector('.next'));

            li.onclick = e=>{
                // console.log(e);
                let x = a.getAttribute('data-page');
                tbody.innerHTML = '';
                x--;
                let start = limit * x;
                let end = start + limit;
                let page = arrayTr.slice(start, end);

                for(let i=0; i<page.length; i++){
                    let item = page[i];
                    tbody.appendChild(item);
                }
            }
        }
    }

    let z = 0;
    function nextElement()
    {
        if(z+limit >= arrayTr.length || z >= arrayTr.length){
            z = -limit;
        }
        if(this.id == 'next'){
            z == arrayTr.length - limit ? (z=0) : (z+= limit);
        }
        if(this.id == 'prev'){
            z == 0 ? arrayTr.length - limit : (z -= limit);
        }

        tbody.innerHTML = '';
        for(let c=z; c < z+limit; c++)
        {
            tbody.appendChild(arrayTr[c]);
        }
        // console.log(z);
    }
    
    document.getElementById('prev').onclick = nextElement;
    document.getElementById('next').onclick = nextElement;
}

// ------------------------------------------------------------------------------------------------------------