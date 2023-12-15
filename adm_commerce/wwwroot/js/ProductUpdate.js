const updateImg = document.querySelector('.update-img');
const fileInput = document.querySelector('.img-input');


fileInput.addEventListener('change', e => {
    let imageFile = e.target.files[0]
    if (!imageFile) return;
    updateImg.setAttribute('src', URL.createObjectURL(imageFile))
})
