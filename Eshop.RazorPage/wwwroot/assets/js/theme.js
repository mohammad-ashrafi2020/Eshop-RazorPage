(function ($) {
    "use strict";
    var HAMTA = {};

    /*====== Preloader ======*/
    var preloader = $(".page-loader");
    $(window).on("load", function () {
        var preloaderFadeOutTime = 500;

        function hidePreloader() {
            preloader.fadeOut(preloaderFadeOutTime);
        }

        hidePreloader();
    });
    /*====== end Preloader ======*/

    /*====== StickyHeader ======*/
    HAMTA.StickyHeader = function () {
        var pageHeaderheight = $('.page-header').height();
        var navWrapperheight = $('.page-header .nav-wrapper').height();
        if ($('.page-header').length) {
            $('.page-content').css('margin-top', pageHeaderheight + navWrapperheight + 15);
        }
        $(window).scroll(function () {
            if ($(this).scrollTop() > 55) {
                $('.page-header').addClass('fixed');
                $('.page-header .top-page-header').slideUp(200);
            } else {
                $('.page-header').removeClass('fixed');
                $('.page-header .top-page-header').slideDown(200);
            }
        });
        var lastScrollTop = 0;
        window.addEventListener('scroll', function () {
            var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
            if (scrollTop > lastScrollTop && !$('.nav-wrapper').hasClass('is-active')) {
                $('.page-header .nav-wrapper').addClass('hidden--nav-wrapper');
            } else {
                $('.page-header .nav-wrapper').removeClass('hidden--nav-wrapper');
            }
            lastScrollTop = scrollTop;
        });
    }
    /*====== end StickyHeader ======*/

    /*====== CategoryList ======*/
    HAMTA.CategoryList = function () {
        $('.nav-wrapper').on('mouseenter', function () {
            $(this).addClass('is-active');
        });
        $('.nav-wrapper').on('mouseleave', function () {
            $(this).removeClass('is-active');
        });
        $('.category-list>ul>li:first-child').addClass('active');
        $('.category-list>ul>li').on('mouseenter', function () {
            $(this).addClass('active').siblings().removeClass('active');
        });
    }
    /*====== end CategoryList ======*/

    /*====== ResponsiveHeader ======*/
    HAMTA.ResponsiveHeader = function () {
        $('.header-responsive .btn-toggle-side-navigation').on('click', function (event) {
            $(this).siblings('.side-navigation').addClass('toggle');
            $(this).siblings('.overlay-side-navigation').fadeIn(200);
        });
        $('.header-responsive .side-navigation ul li.has-children > a').on('click', function (event) {
            event.preventDefault();
            $(this).toggleClass('active');
            $(this).siblings('ul').slideToggle(100);
        });
        $('.header-responsive .overlay-side-navigation').on('click', function (event) {
            $(this).siblings('.side-navigation').removeClass('toggle');
            $(this).fadeOut(200);
        });
    }
    /*====== end ResponsiveHeader ======*/

    /*====== SearchResult ======*/
    HAMTA.SearchResult = function () {
        $('.search-box form input').on('click', function () {
            $(this).parents('.search-box').addClass('show-result').find('.search-result').fadeIn();
        })
        $('.search-box form input').keyup(function () {
            $(this).parents('.search-box').addClass('show-result').find('.search-result').fadeIn();
            $(this).parents('.search-box').find('.search-result-list').fadeIn();
            if ($(this).val().length == 0) {
                // Hide the element
                $(this).parents('.search-box').removeClass('show-result').find('.search-result').fadeOut(100);
                $(this).parents('.search-box').find('.search-result-list').fadeOut();
                $(this).parents('.search-box').removeClass('show-result');
            } else {
                // Otherwise show it
                $(this).parents('.search-box').find('.search-result').fadeIn(100);
                $(this).parents('.search-box').find('.search-result-list').fadeIn();
            }
        });
        $(document).click(function (e) {
            if ($(e.target).is('.search-box *')) return;
            $('.search-result').hide();
            $('.search-box').removeClass('show-result');
        });
    }
    /*====== end SearchResult ======*/

    /*====== SweetAlert ======*/
    HAMTA.SweetAlert = function () {
        $('.user-item.cart-list > ul li.cart-items ul .cart-item .remove-item').on('click', function () {
            Swal.fire({
                text: "از سبد خریدتان حذف شود؟",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'بله',
                cancelButtonText: 'خیر'
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire({
                        title: 'حذف شد!',
                        confirmButtonText: 'باشه',
                        icon: 'success'
                    })
                }
            })
        });
        $('.product-card .product-card-bottom .btn-add-to-cart').on('click', function (event) {
            event.preventDefault();
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            Toast.fire({
                icon: 'success',
                title: 'به سبد خریدتان اضافه شد'
            })
        });
        $('.product-card .product-card-actions .add-to-wishlist').on('click', function (event) {
            event.preventDefault();
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            Toast.fire({
                icon: 'success',
                title: 'به لیست علاقمندی اضافه شد'
            })
        });
        $('.product-card .product-card-actions .add-to-compare').on('click', function (event) {
            event.preventDefault();
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            Toast.fire({
                icon: 'success',
                title: 'برای مقایسه اضافه شد'
            })
        });
    }
    /*====== end SweetAlert ======*/

    /*====== NiceScroll ======*/
    HAMTA.NiceScroll = function () {
        $('.do-nice-scroll').niceScroll({
            railalign: 'left',
        });
    }
    /*====== end NiceScroll ======*/

    /*====== BackToTop ======*/
    HAMTA.BackToTop = function () {
        $(".back-to-top .back-btn").click(function () {
            $("body,html").animate({
                scrollTop: 0
            }, 700);
            return false;
        });
    }
    /*====== end BackToTop ======*/

    /*====== Tooltip ======*/
    HAMTA.Tooltip = function () {
        $('[data-toggle="tooltip"]').tooltip();
    };
    /* end Tooltip ======*/

    /*====== productAddTo ======*/
    HAMTA.ProductAddTo = function () {
        if ($('.product-card-actions').length) {
            $('.product-card-actions button').on('click', function () {
                $(this).toggleClass('added');
            });
        }
        $('.product--actions .is-action').on('click', function (event) {
            event.preventDefault();
            $(this).toggleClass('added');
        });
    }
    /*====== end productAddTo ======*/

    /*====== Select2 ======*/
    HAMTA.Select2 = function () {
        if ($('.select2').length) {
            $('.select2').select2({
                dir: "rtl"
            });
        }
    }
    /*====== end Select2 ======*/

    /*====== CollapseWidget ======*/
    HAMTA.CollapseWidget = function () {
        $('.btn-collapse').on("click", function () {
            var btnCollapse = $(this);
            btnCollapse.toggleClass('btn-collapsed');
            btnCollapse.parent('.sidebar-widget-title').siblings('.collpase-widget').slideToggle();
        });
    }
    /*====== end CollapseWidget ======*/

    /*====== SwiperSlider ======*/
    HAMTA.SwiperSlider = function () {
        if ($('.main-page-slider').length) {
            var mainSlider = new Swiper('.main-page-slider', {
                navigation: {
                    nextEl: '.swiper-button-next',
                    prevEl: '.swiper-button-prev',
                },
                autoplay: {
                    delay: 2000,
                },
                loop: true,
                loopedSlides: 4
            });
            var mainPageSliderThumbs = new Swiper('.main-page-slider-thumbs', {
                slidesPerView: 4,
                touchRatio: 0.2,
                slideToClickedSlide: true,
                loop: true,
                loopedSlides: 4
            });
            mainSlider.controller.control = mainPageSliderThumbs;
            mainPageSliderThumbs.controller.control = mainSlider;
        }
        if ($('.offer-slider').length) {
            var offerSlider = new Swiper('.offer-slider', {
                effect: 'fade',
                centeredSlides: true,
                autoplay: {
                    delay: 2000,
                },
            });
            var offerSliderThumbs = new Swiper('.offer-slider-thumbs', {
                slidesPerView: 5,
                touchRatio: 0.2,
                slideToClickedSlide: true,
                centeredSlides: true,
                navigation: {
                    nextEl: '.offer-slider-button-next',
                    prevEl: '.offer-slider-button-prev',
                },
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true
                },
            });
            offerSlider.controller.control = offerSliderThumbs;
            offerSliderThumbs.controller.control = offerSlider;
        }
        var sliderSingle = new Swiper('.slider-single', {
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            pagination: {
                el: '.swiper-pagination',
                clickable: true,
                dynamicBullets: true,
            },
            autoplay: {
                delay: 2000,
            },
            effect: 'fade',
            loop: true,
        });
        var sliderLg = new Swiper('.slider-lg', {
            speed: 800,
            spaceBetween: 10,
            observer: true,
            observeParents: true,
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            breakpoints: {
                1400: {
                    slidesPerView: 6,
                },
                1200: {
                    slidesPerView: 5,
                },
                992: {
                    slidesPerView: 4,
                },
                768: {
                    slidesPerView: 3.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                576: {
                    slidesPerView: 2.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                480: {
                    slidesPerView: 2.2,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 8,
                },
                0: {
                    slidesPerView: 1.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 8,
                }
            }
        });
        var sliderMd = new Swiper('.slider-md', {
            speed: 800,
            spaceBetween: 10,
            observer: true,
            observeParents: true,
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            breakpoints: {
                992: {
                    slidesPerView: 4,
                },
                768: {
                    slidesPerView: 3.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                576: {
                    slidesPerView: 2.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                480: {
                    slidesPerView: 2.2,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 8,
                },
                0: {
                    slidesPerView: 1.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 8,
                }
            }
        });
        var sliderAmazing = new Swiper('.slider-amazing', {
            speed: 800,
            spaceBetween: 10,
            observer: true,
            observeParents: true,
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            breakpoints: {
                1400: {
                    slidesPerView: 5,
                },
                1060: {
                    slidesPerView: 4,
                },
                800: {
                    slidesPerView: 3,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                700: {
                    slidesPerView: 2,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                576: {
                    slidesPerView: 1.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                }
            }
        });
        var sliderSm = new Swiper('.slider-sm', {
            speed: 800,
            spaceBetween: 10,
            observer: true,
            observeParents: true,
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            breakpoints: {
                992: {
                    slidesPerView: 3,
                },
                768: {
                    slidesPerView: 2.5,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                576: {
                    slidesPerView: 2,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 10,
                },
                480: {
                    slidesPerView: 1.2,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 8,
                },
                0: {
                    slidesPerView: 1,
                    scrollbar: {
                        el: '.swiper-scrollbar',
                        hide: true,
                    },
                    spaceBetween: 8,
                }
            }
        });
        if ($('.gallery-slider').length) {
            var gallerySlider = new Swiper('.gallery-slider', {
                loop: true,
                loopedSlides: 4
            });
            var gallerySliderThumbs = new Swiper('.gallery-slider-thumbs', {
                slidesPerView: 4,
                touchRatio: 0.2,
                slideToClickedSlide: true,
                loop: true,
                loopedSlides: 4,
                navigation: {
                    nextEl: '.swiper-button-next',
                    prevEl: '.swiper-button-prev',
                },
            });
            gallerySlider.controller.control = gallerySliderThumbs;
            gallerySliderThumbs.controller.control = gallerySlider;
        }
        if ($('.quick-view-gallery-slider').length) {
            var quickViewGallerySlider = new Swiper('.quick-view-gallery-slider', {
                loop: true,
                loopedSlides: 4
            });
            var quickViewGallerySliderThumbs = new Swiper('.quick-view-gallery-slider-thumbs', {
                slidesPerView: 4,
                touchRatio: 0.2,
                slideToClickedSlide: true,
                loop: true,
                loopedSlides: 4,
                navigation: {
                    nextEl: '.swiper-button-next',
                    prevEl: '.swiper-button-prev',
                },
            });
            quickViewGallerySlider.controller.control = quickViewGallerySliderThumbs;
            quickViewGallerySliderThumbs.controller.control = quickViewGallerySlider;
            $('#quickViewModal').on('shown.bs.modal', function () {
                quickViewGallerySlider.update();
                quickViewGallerySliderThumbs.update();
            });
        }
    }
    /*====== end SwiperSlider ======*/

    /*====== Product +/- ======*/
    //HAMTA.Quantity = function () {
    //    $('.num-in span').click(function () {
    //        var $input = $(this).parents('.num-block').find('input.in-num');
    //        if ($(this).hasClass('minus')) {
    //            var count = parseFloat($input.val()) - 1;
    //            count = count < 1 ? 1 : count;
    //            if (count < 2) {
    //                $(this).addClass('dis');
    //            } else {
    //                $(this).removeClass('dis');
    //            }
    //            $input.val(count);
    //        } else {
    //            var count = parseFloat($input.val()) + 1;
    //            $input.val(count);
    //            if (count > 1) {
    //                $(this).parents('.num-block').find(('.minus')).removeClass('dis');
    //            }
    //        }

    //        $input.change();
    //        return false;
    //    });
    //};
    /* end Product +/- ======*/

    /*====== Filter price ======*/
    HAMTA.FilterPrice = function () {
        if ($('.filter-price').length) {
            var skipSlider = document.getElementById('slider-non-linear-step');
            var $sliderFrom = document.querySelector('.js-slider-range-from');
            var $sliderTo = document.querySelector('.js-slider-range-to');
            var min = parseInt($sliderFrom.dataset.range),
                max = parseInt($sliderTo.dataset.range);
            noUiSlider.create(skipSlider, {
                start: [$sliderFrom.value, $sliderTo.value],
                connect: true,
                direction: 'rtl',
                format: wNumb({
                    thousand: ',',
                    decimals: 0,
                }),
                step: 1,
                range: {
                    'min': min,
                    'max': max
                }
            });
            var skipValues = [
                document.getElementById('skip-value-lower'),
                document.getElementById('skip-value-upper')
            ];
            skipSlider.noUiSlider.on('update', function (values, handle) {
                skipValues[handle].value = values[handle];
            });
        }
    };
    /* end Filter price ======*/

    /*====== Filter Options ======*/
    HAMTA.FilterOptions = function () {
        if ($('.search-in-options').length) {
            $(".search-in-options input[type=text]").on("keyup", function () {
                var value = $(this).val();
                $(this).parents('.search-in-options').siblings('.widget-content').find('.container-checkbox').filter(function () {
                    $(this).toggle($(this).text().indexOf(value) > -1)
                });
            });
        }
    };
    /* end Filter Options ======*/

    /*====== sidebar-sticky ======*/
    HAMTA.StickySidebar = function () {
        if ($('.container .sticky-sidebar').length) {
            $('.container .sticky-sidebar').theiaStickySidebar();
        }
    };
    /* end sidebar-sticky ======*/

    /*====== filter-options-sidebar ======*/
    HAMTA.FilterOptionsSidebar = function () {
        $('.btn-filter-sidebar').on('click', function () {
            $('.filter-options-sidebar').addClass('toggled');
        });
        $('.btn-close-filter-sidebar').on('click', function () {
            $('.filter-options-sidebar').removeClass('toggled');
        });
    };
    /* end filter-options-sidebar ======*/

    /*====== responsive tab order list ======*/
    HAMTA.ResponsiveTabOrderList = function () {
        $('.tab-responsive-order-list .dropdown-item').on('click', function () {
            $(this).siblings('a').removeClass('active');
            $(this).parents(".dropdown").find('.btn').html($(this).text() + ' <span class="caret"></span>');
            $(this).parents(".dropdown").find('.btn').val($(this).data('value'));
        });
    };
    /* end responsive tab order list ======*/

    /*====== zoom ======*/
    HAMTA.ZoomImage = function () {
        if ($('.zoom-image').length) {
            $('.zoom-image').ezPlus({
                zoomType: 'lens',
                lensShape: 'round',
                lensSize: 170,
                borderColour: '#fff'
            });
        }
    };
    /* end zoom ======*/

    /*====== color switcher ======*/
    HAMTA.ColorSwitcher = function () {
        if ($('#colorswitch-option').length) {
            $('#colorswitch-option button').on('click', function () {
                $('#colorswitch-option ul').toggleClass('show');
            });
            $('#colorswitch-option ul li').on('click', function () {
                $('#colorswitch-option ul li').removeClass('active');
                $(this).addClass('active');
                var colorPath = $(this).attr("data-path");
                $('#colorswitch').attr('href', colorPath);
            });
        }
    }
    /* end color switcher ======*/

    $(window).on("load", function () {});
    $(document).ready(function () {
        HAMTA.StickyHeader(), HAMTA.CategoryList(), HAMTA.ResponsiveHeader(), HAMTA.SearchResult(), HAMTA.SweetAlert(), HAMTA.NiceScroll(), HAMTA.BackToTop(), HAMTA.Tooltip(), HAMTA.CollapseWidget(), HAMTA.ProductAddTo(), HAMTA.Select2(), HAMTA.SwiperSlider(), HAMTA.FilterPrice(), HAMTA.FilterOptions(), HAMTA.StickySidebar(), HAMTA.FilterOptionsSidebar(), HAMTA.ResponsiveTabOrderList(), HAMTA.ZoomImage(), HAMTA.ColorSwitcher();
    });
})(jQuery);