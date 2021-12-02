function hide_policy(){
    document.getElementById('privacy-policy').style.display = "none";
}

$(document).ready(function(){
    $(window).scroll(function(){
        if(this.scrollY > 20 ){
            $(".navbar").addClass("sticky");
        }
        else{
            $(".navbar").removeClass("sticky");
        }
    });

    $('.menu-toggler').click(function(){
        $(this).toggleClass("active");
        $('.custom-navbar-menu').toggleClass("active");
    });
});