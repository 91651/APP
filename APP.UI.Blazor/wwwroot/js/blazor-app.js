window.appjs = {
    canvas :{
        drawImage: (el, src) => {
            let img = new Image();
            img.src = src;
            img.onload = () => {
                let canvasCaptcha = el;
                var ctxCaptcha = canvasCaptcha.getContext('2d');
                ctxCaptcha.clearRect(0, 0, 999, 999)
                ctxCaptcha.drawImage(img, 0, 0);
            }
        }
    }
}
