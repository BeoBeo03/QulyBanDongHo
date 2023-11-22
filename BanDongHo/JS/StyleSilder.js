
$(document).ready(function () {
    $(".owl-carousel").owlCarousel();
});

$('.owl-carousel').owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    autoplay: true,
    autoplayTimeout: 2000,
    autoplayHoverPause: true,
    responsive: {
        0: {
            items: 2,
            margin: 50
        },
        390: {
            items: 1,
            margin: 70
        },
        412: {
            items: 2,
            margin: 62
        },
        600: {
            items: 3,
            margin: 60
        },
        1024: {
            items: 4,
            margin: 80

        }
    }
});
$(".owl-item").css("width", "180px")
$(".owl-stage").css("height", "400px")

// 

$(document).ready(function () {
    $("#flip").click(function () {
        $("#panel").slideToggle("slow");
    });

});
//


//
let slideIndex = 0;
showSlides();

function showSlides() {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 2000); // Change image every 2 seconds
}

//
