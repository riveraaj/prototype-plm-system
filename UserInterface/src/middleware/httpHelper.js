export const httpHelper = () => {
    const customFetch = async (endpoint, options) => {
        const defaultHeader = {
            accept: 'application/json',
            'content-type': 'application/json',
        }

        const controller = new AbortController();
        options.signal = controller.signal;

        options.method = options.method || 'GET';
        options.headers = options.headers ? { ...defaultHeader, ...options.headers } : defaultHeader;

        options.body = typeof options.body === 'object' ? JSON.stringify(options.body) : options.body;

        if (!options.body) delete options.body;

        setTimeout(() => controller.abort(), 5000);

        try {
            const res = await fetch(endpoint, options);
            return await res.json();
        } catch (ex) {
            return ex;
        }
    };

    const get = (url, options = {}) => customFetch(url, options);

    const post = (url, options = {}) => {
        options.method = 'POST';
        return customFetch(url, options);
    };

    const put = (url, options = {}) => {
        options.method = 'PUT';
        return customFetch(url, options);
    };

    const del = (url, options = {}) => {
        options.method = 'DELETE';
        return customFetch(url, options);
    };

    return { get, post, put, del }
};