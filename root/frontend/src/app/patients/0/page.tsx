// src/app/patients/[id]/page.tsx
"use client";

import { useParams, useRouter } from "next/navigation";
import { useQuery } from "@apollo/client/react";
import { GET_PATIENT_DETAIL } from "@/graphql/queries";
import {
  Box,
  Typography,
  Paper,
  Stack,
  Button,
  CircularProgress,
  Alert,
  Divider,
  Grid
} from "@mui/material";

type DiagnosticEntry = {
  id: number;
  recordedAt: string;
  diagnosis: string;
  notes?: string | null;
};

type PatientDetail = {
  id: number;
  fullName: string;
  age: number;
  lastDiagnosis?: string | null;
  diagnosticEntries: DiagnosticEntry[];
};

export default function PatientDetailPage() {
  const params = useParams();
  const router = useRouter();
  const id = Number(params.id);

  const { data, loading, error } = useQuery<{ patientById: PatientDetail }>(
    GET_PATIENT_DETAIL,
    { variables: { id } }
  );

  if (loading)
    return (
      <Box display="flex" justifyContent="center" mt={4}>
        <CircularProgress />
      </Box>
    );

  if (error)
    return (
      <Alert severity="error" sx={{ mt: 4 }}>
        Chyba: {error.message}
      </Alert>
    );

  if (!data?.patientById)
    return (
      <Alert severity="warning" sx={{ mt: 4 }}>
        Pacient nenalezen.
      </Alert>
    );

  const p = data.patientById;

  return (
    <Stack spacing={3}>
      <Button variant="text" onClick={() => router.back()}>
        ← Zpět na seznam
      </Button>

      <Grid container spacing={3}>
        <Grid size={{xs:12, md:6}}>
          <Paper elevation={1} sx={{ p: 3 }}>
            <Typography variant="h5" gutterBottom>
              {p.fullName}
            </Typography>
            <Typography variant="body1">Věk: {p.age}</Typography>
            <Typography variant="body1" mt={1}>
              Poslední diagnóza: <strong>{p.lastDiagnosis ?? "N/A"}</strong>
            </Typography>
          </Paper>
        </Grid>

        <Grid size={{xs:12, md:6}}>
          <Paper elevation={1} sx={{ p: 3 }}>
            <Typography variant="subtitle1" gutterBottom>
              Statický obrázek vyšetření (MRI mozku)
            </Typography>
            <Box
              sx={{
                borderRadius: 2,
                overflow: "hidden",
                bgcolor: "grey.900",
                display: "flex",
                justifyContent: "center",
              }}
            >
              <img
                src="/mri-brain.png"
                alt="MRI mozku"
                style={{ width: "100%", height: "auto", objectFit: "cover" }}
              />
            </Box>
          </Paper>
        </Grid>
      </Grid>

      <Paper elevation={1} sx={{ p: 3 }}>
        <Typography variant="h6" gutterBottom>
          Diagnostické údaje v čase
        </Typography>
        {p.diagnosticEntries.length === 0 ? (
          <Typography variant="body2" color="text.secondary">
            Zatím nejsou žádné záznamy.
          </Typography>
        ) : (
          <Stack spacing={2} mt={1}>
            {p.diagnosticEntries
              .slice()
              .sort((a, b) => a.recordedAt.localeCompare(b.recordedAt))
              .map((d, index) => (
                <Box key={d.id}>
                  {index > 0 && <Divider sx={{ mb: 1 }} />}
                  <Typography variant="subtitle2">
                    {new Date(d.recordedAt).toLocaleString("cs-CZ")}
                  </Typography>
                  <Typography variant="body2">{d.diagnosis}</Typography>
                  {d.notes && (
                    <Typography variant="body2" color="text.secondary">
                      Poznámka: {d.notes}
                    </Typography>
                  )}
                </Box>
              ))}
          </Stack>
        )}
      </Paper>
    </Stack>
  );
}