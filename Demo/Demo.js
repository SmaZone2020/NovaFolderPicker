const fs = require('fs');
const path = require('path');
const { execFileSync } = require('child_process');

if (fs.existsSync('NovaFP.exe')) {
    try {
        execFileSync('NovaFP.exe', ['C:\\'], {
            stdio: 'ignore'
        });

        const tempFilePath = path.join(require('os').tmpdir(), '.temp');

        if (fs.existsSync(tempFilePath)) {
            const tempPath = fs.readFileSync(tempFilePath, 'utf8').trim();
            console.log('Selected Folder:', tempPath);

            fs.unlinkSync(tempFilePath);
        }
    } catch (error) {
        console.error('Error starting NovaFP.exe:', error);
    }
} else {
    console.log('NovaFP.exe not found!');
}
