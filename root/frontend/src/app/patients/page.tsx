// src/app/patients/page.tsx
"use client";

import { useQuery } from "@apollo/client/react";
import { GET_PATIENTS } from "@/graphql/queries";
import { useRouter } from "next/navigation";
import {
  Box,
  Typography,
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  CircularProgress,
  Alert,
  Stack,
  TableContainer,
} from "@mui/material";

type Patient = {
  id: number;
  fullName: string;
  age: number;
  lastDiagnosis?: string | null;
};

export default function PatientsPage() {
  const { data, loading, error } = useQuery<{ patients: Patient[] }>(GET_PATIENTS);
  const router = useRouter();

  if (loading)
    return (
      <Box display="flex" justifyContent="center" mt={4}>
        <CircularProgress />
      </Box>
    );

  if (error)
    return (
      <Alert severity="error" sx={{ mt: 4 }}>
        Chyba při načítání pacientů: {error.message}
      </Alert>
    );

  return (
    <Stack spacing={3}>
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Typography variant="h4" component="h1">
          Pacienti
        </Typography>
        <Button
          variant="contained"
          color="primary"
          onClick={() => router.push("/patients/new")}
        >
          Nový pacient
        </Button>
      </Box>

      <TableContainer component={Paper} elevation={1}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>Jméno</TableCell>
              <TableCell>Věk</TableCell>
              <TableCell>Poslední diagnóza</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data?.patients.map((p) => (
              <TableRow
                key={p.id}
                hover
                sx={{ cursor: "pointer" }}
                onClick={() => router.push(`/patients/${p.id}`)}
              >
                <TableCell>{p.fullName}</TableCell>
                <TableCell>{p.age}</TableCell>
                <TableCell>
                  {p.lastDiagnosis ?? (
                    <Typography variant="body2" color="text.secondary" fontStyle="italic">
                      N/A
                    </Typography>
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Stack>
  );
}