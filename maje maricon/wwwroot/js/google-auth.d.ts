interface GoogleCredentialResponse {
    credential: string;
    select_by?: string;
}
declare function handleCredentialResponse(response: GoogleCredentialResponse): Promise<void>;
declare function signOut(): void;
declare global {
    interface Window {
        handleCredentialResponse: typeof handleCredentialResponse;
        signOut: typeof signOut;
    }
}
export {};
//# sourceMappingURL=google-auth.d.ts.map