const fs = require('fs').promises;
const path = require('path');

const inputsRoot = 'inputs';

const INPUT_FILENAME = 'input.txt';
const downloadChallengeInput = async (sessionId, year, day) => {
  const paddedDay = day.toString().padStart(2, '0');

  const res = await fetch(`https://adventofcode.com/${year}/day/${day}/input`, {
    method: 'GET',
    credentials: 'include',
    headers: {
      Cookie: `session=${sessionId}`,
    },
  });

  const txt = await res.text();
  const destDir = path.join(inputsRoot, year.toString(), paddedDay);
  const dest = path.resolve(destDir, INPUT_FILENAME);

  await fs.mkdir(destDir, { recursive: true });
  await fs.writeFile(dest, txt.replace(/\r|\n|\r\n/g, '\r\n').trim(), 'utf-8');

  console.log(`Downloaded AOC ${year} day ${day} successfully.`);
};

const fileExists = async (filePath) => {
  try {
    await fs.stat(filePath);
    return true;
  } catch {
    return false;
  }
};

const getExistingInputs = async () => {
  const years = await fs.readdir(inputsRoot);
  const existing = [];

  for (const year of years) {
    const days = await fs.readdir(path.join(inputsRoot, year));
    for (const day of days) {
      if (await fileExists(path.join(inputsRoot, year, day, INPUT_FILENAME))) {
        existing.push(`${year}:${parseInt(day)}`);
      }
    }
  }

  return existing;
};

(async function main() {
  const sessionId = process.argv[2];

  if (!sessionId) throw new Error('No Session ID');

  const today = new Date();
  const existing = await getExistingInputs();

  const promises = [];

  for (let year = 2015; year <= today.getFullYear(); year++) {
    for (let day = 1; day <= 25; day++) {
      const key = `${year}:${day}`;

      if (existing.includes(key)) continue;
      if (new Date(year, 11, day) > today) break;

      promises.push(downloadChallengeInput(sessionId, year, day));
    }
  }

  if (!promises.length) {
    console.log('Up to date.');
    return;
  }

  await Promise.all(promises);
})();
