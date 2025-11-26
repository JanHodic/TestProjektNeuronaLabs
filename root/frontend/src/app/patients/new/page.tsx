// src/app/patients/new/page.tsx
"use client";

import { useState, FormEvent } from "react";
import { useRouter } from "next/navigation";
import { useMutation } from "@apollo/client/react";
import { CREATE_PATIENT } from "@/graphql/mutations";
import {
  Box,
  Paper,
  TextField,
  Typography,
  Button,
  Stack,
  Alert,
} from "@mui/material";

export default function NewPatientPage() {
  const [fullName, setFullName] = useState("");
  const [age, setAge] = useState<string>("");
  const [lastDiagnosis, setLastDiagnosis] = useState("");
  const router = useRouter();

  const [createPatient, { loading, error }] = useMutation(CREATE_PATIENT);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();

    await createPatient({
      variables: {
        input: {
          fullName,
          age: Number(age),
          lastDiagnosis: lastDiagnosis || null,
        },
      },
    });

    router.push("/patients");
  };

  return (
    <Box maxWidth="sm" mx="auto">
      <Paper elevation={1} sx={{ p: 3 }}>
        <Typography variant="h5" mb={2}>
          Nový pacient
        </Typography>

        <form onSubmit={handleSubmit}>
          <Stack spacing={2}>
            <TextField
              label="Jméno a příjmení"
              value={fullName}
              onChange={(e) => setFullName(e.target.value)}
              required
              fullWidth
            />
            <TextField
              label="Věk"
              type="number"
              value={age}
              onChange={(e) => setAge(e.target.value)}
              required
              fullWidth
            />
            <TextField
              label="Poslední diagnóza (volitelné)"
              value={lastDiagnosis}
              onChange={(e) => setLastDiagnosis(e.target.value)}
              fullWidth
            />

            {error && (
              <Alert severity="error">
                Chyba při vytváření pacienta: {error.message}
              </Alert>
            )}

            <Box display="flex" justifyContent="flex-end" gap={1}>
              <Button variant="outlined" onClick={() => router.back()}>
                Zrušit
              </Button>
              <Button type="submit" variant="contained" disabled={loading}>
                {loading ? "Ukládám…" : "Vytvořit"}
              </Button>
            </Box>
          </Stack>
        </form>
      </Paper>
    </Box>
  );
}
