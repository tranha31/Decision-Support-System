export default class Toast{
    /**
     * Khởi tạo toast
     * @param {String} action : Trạng thái hành động
     * @param {String} content: Nội dung thông báo
     * @param {String} type: Loại thông báo
     * @param {Number} duration: Thời gian tồn tại thông báo 
     * create by: TQHa (24/10/2021)
     */
    toast({action, content, type, duration }) {
        const main = document.getElementById("toast");
        if (main) {
            const toast = document.createElement("div");
            //auto remove toast
            const autoRemove = setTimeout(function () {
            main.removeChild(toast);
            }, duration + 1000);
            toast.onclick = function (e) {
            if (e.target.closest(".delete-toast")) {
                main.removeChild(toast);
                clearTimeout(autoRemove);
            }
            };
            const delay = (duration / 1000).toFixed(2);
            
            toast.classList.add("toast-message");
            toast.style.animation = `slideInLeft ease .5s, fadeOut linear 1s ${delay}s forwards`;
            toast.innerHTML = `
                <div class="mi mi-16 delete-toast"></div>
                <p><span class="${type}">${action}&nbsp;</span>${content}</p>
            `;
            main.appendChild(toast);
        }
    }

    constructor(){

    }
}