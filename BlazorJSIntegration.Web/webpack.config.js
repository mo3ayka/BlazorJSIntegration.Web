const path = require('path');

const bundleFileName = 'blazor.integration';
const dirName = './wwwroot';

module.exports = (env, argv) => {
    return {
        mode: argv.mode === 'production' ? 'production' : 'development',
        entry: [
            './Scripts',
        ],
        resolve: {
            extensions: ['.tsx', '.ts', '.js'],
        },
        output: {
            path: path.resolve(__dirname, dirName),
            filename: bundleFileName + '.js',
        },
        module: {
            rules: [
                {
                    test: /\.ts?$/,
                    exclude: /node_modules/,
                    use: {
                        loader: "ts-loader"
                    }
                },
                {
                    test: /\.css$/,
                    use: ['style-loader', 'css-loader'],
                },
            ]
        }
    }
};
