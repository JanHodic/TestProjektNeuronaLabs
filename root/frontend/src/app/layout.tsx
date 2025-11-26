// src/app/layout.tsx
"use client";

import type { ReactNode } from "react";
import { ApolloProvider } from "@apollo/client/react";
import client from "../libs/apolloClient";
import {
  CssBaseline,
  ThemeProvider,
  createTheme,
  Container,
  Box,
  AppBar,
  Toolbar,
  Typography,
} from "@mui/material";

const theme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#1976d2",
    },
    background: {
      default: "#f3f4f6",
    },
  },
});

export default function RootLayout({ children }: { children: ReactNode }) {
  return (
    <html lang="cs">
      <body>
        <ApolloProvider client={client}>
          <ThemeProvider theme={theme}>
            <CssBaseline />
            <Box sx={{ minHeight: "100vh", bgcolor: "background.default" }}>
              <AppBar position="static" elevation={1}>
                <Toolbar>
                  <Typography variant="h6" component="div">
                    Neurona – Správa pacientů
                  </Typography>
                </Toolbar>
              </AppBar>
              <Container maxWidth="lg" sx={{ py: 4 }}>
                {children}
              </Container>
            </Box>
          </ThemeProvider>
        </ApolloProvider>
      </body>
    </html>
  );
}
