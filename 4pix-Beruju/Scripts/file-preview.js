let currentZoom = 1;
let currentRotation = 0;
let currentFileType = '';
let currentFileUrl = '';

function resetPreviewState() {
    currentZoom = 1;
    currentRotation = 0;
    currentFileType = '';
    currentFileUrl = '';
}

function getFileNameFromPath(path) {
    return path.split('/').pop();
}

function getFileIcon(ext) {
    ext = ext.toLowerCase();

    if (['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp'].includes(ext)) return 'fa-regular fa-image text-info';
    if (ext === 'pdf') return 'fa-regular fa-file-pdf text-danger';
    if (['doc', 'docx'].includes(ext)) return 'fa-regular fa-file-word text-primary';
    if (['xls', 'xlsx', 'csv'].includes(ext)) return 'fa-regular fa-file-excel text-success';
    if (['ppt', 'pptx'].includes(ext)) return 'fa-regular fa-file-powerpoint text-warning';
    if (['txt', 'log', 'json', 'xml'].includes(ext)) return 'fa-regular fa-file-lines text-secondary';

    return 'fa-regular fa-file';
}

function previewFile(fileUrl, containerId = "filePreview") {

    resetPreviewState();

    currentFileUrl = fileUrl;
    const ext = fileUrl.split('.').pop().toLowerCase();
    currentFileType = ext;

    const previewDiv = document.getElementById(containerId);
    const fileName = getFileNameFromPath(fileUrl);
    const iconClass = getFileIcon(ext);

    previewDiv.innerHTML = `
        <div class="preview-toolbar">

            <a href="${fileUrl}" download class="btn btn-success btn-sm">
                <i class="fa-solid fa-download"></i>
            </a>

            <a href="${fileUrl}" target="_blank" class="btn btn-outline-primary btn-sm">
                <i class="fa-solid fa-up-right-from-square"></i>
            </a>

            <button class="btn btn-outline-secondary btn-sm" onclick="zoomIn()">
                <i class="fa-solid fa-magnifying-glass-plus"></i>
            </button>

            <button class="btn btn-outline-secondary btn-sm" onclick="zoomOut()">
                <i class="fa-solid fa-magnifying-glass-minus"></i>
            </button>

            <button id="rotateLeftBtn" class="btn btn-outline-warning btn-sm" onclick="rotateLeft()" style="display:none;">
                <i class="fa-solid fa-rotate-left"></i>
            </button>

            <button id="rotateRightBtn" class="btn btn-outline-warning btn-sm" onclick="rotateRight()" style="display:none;">
                <i class="fa-solid fa-rotate-right"></i>
            </button>

            <button class="btn btn-outline-secondary btn-sm" onclick="resetZoom()">
                <i class="fa-solid fa-arrows-rotate"></i>
            </button>
        </div>

        <div class="preview-stage" id="previewStage"></div>
    `;

    const stage = document.getElementById("previewStage");

    // IMAGE
    if (['jpg', 'jpeg', 'png', 'gif', 'webp'].includes(ext)) {

        document.getElementById('rotateLeftBtn').style.display = 'inline-block';
        document.getElementById('rotateRightBtn').style.display = 'inline-block';

        stage.innerHTML = `<img id="previewImage" src="${fileUrl}" />`;
    }

    // PDF
    else if (ext === 'pdf') {
        stage.innerHTML = `
            <div id="pdfWrapper" style="transform:scale(1); transform-origin: top center;">
                <embed src="${fileUrl}" type="application/pdf" />
            </div>
        `;
    }

    // TEXT
    else if (['txt', 'json', 'xml', 'csv'].includes(ext)) {
        fetch(fileUrl)
            .then(r => r.text())
            .then(txt => {
                stage.innerHTML = `<pre>${escapeHtml(txt)}</pre>`;
            });
    }

    // OTHER
    else {
        stage.innerHTML = `
            <div class="unsupported-box text-center">
                <i class="${iconClass} fa-2x"></i>
                <p>Preview not supported</p>
            </div>
        `;
    }
}

function escapeHtml(str) {
    return str.replace(/</g, "&lt;").replace(/>/g, "&gt;");
}

function applyTransform() {
    const img = document.getElementById("previewImage");
    if (img) {
        img.style.transform = `scale(${currentZoom}) rotate(${currentRotation}deg)`;
    }

    const pdf = document.getElementById("pdfWrapper");
    if (pdf) {
        pdf.style.transform = `scale(${currentZoom})`;
    }
}

function zoomIn() { currentZoom += 0.1; applyTransform(); }
function zoomOut() { currentZoom -= 0.1; applyTransform(); }
function resetZoom() { currentZoom = 1; currentRotation = 0; applyTransform(); }
function rotateLeft() { currentRotation -= 90; applyTransform(); }
function rotateRight() { currentRotation += 90; applyTransform(); }